using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Radia.Services.FileProviders.Local;
using System.Security.Cryptography;
using System.Text;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileProvider : IRadiaFileProvider
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
            string path = GitFileProvider.GetCacheFolder(args.LocalCache);
            this.radiaFileProvider = new LocalFileProvider(path, allowListing);
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
    }
}
