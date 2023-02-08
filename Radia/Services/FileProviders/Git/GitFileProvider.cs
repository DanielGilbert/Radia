using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Radia.Services.FileProviders.Local;
using System.Security.Cryptography;
using System.Text;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileProvider : IRadiaFileProvider, IRadiaNetworkFileProvider
    {
        private readonly string repositoryAddress;
        private readonly string branch;
        private readonly string localCache;
        private readonly IRadiaFileProvider radiaFileProvider;

        public GitFileProvider(GitFileProviderSettings args, bool allowListing)
        {
            this.repositoryAddress = args.Repository;
            this.branch = args.Branch;
            this.localCache = GetCacheFolder(args.LocalCache);
            CloneOptions cloneOptions = new()
            {
                BranchName = this.branch
            };
            try
            {
                _ = Repository.Clone(this.repositoryAddress, this.localCache, cloneOptions);
            }
            catch (NameConflictException)
            {
            }
            this.radiaFileProvider = new LocalFileProvider(this.localCache, allowListing);
        }

        public IRadiaDirectoryContents GetDirectoryContents(string subpath)
        {
            return this.radiaFileProvider.GetDirectoryContents(subpath);
        }

        public IRadiaFileInfo GetFileInfo(string subpath)
        {
            return this.radiaFileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return this.radiaFileProvider.Watch(filter);
        }

        public static String GetCacheFolder(string value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return $"/gitTmp/{String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")))}";
            }
        }

        public void Fetch()
        {
            string logMessage = "";
            using (var repo = new Repository(this.localCache))
            {
                var remote = repo.Network.Remotes["origin"];
                var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);
                var branch = repo.Branches[this.branch];

                if (branch == null)
                {
                    // repository return null object when branch not exists
                    return;
                }

                Branch currentBranch = Commands.Checkout(repo, branch);
            }
        }
    }
}
