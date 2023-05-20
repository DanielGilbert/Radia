using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Radia.Services.FileProviders.Local;
using System.Security.Cryptography;
using System.Text;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileProvider : IRadiaFileProvider, IRadiaNetworkFileProvider, IDisposable
    {
        private readonly bool allowListing;
        private readonly string repositoryAddress;
        private readonly string branch;
        private readonly string localCache;
        private bool disposedValue;
        private readonly Repository repository;

        public GitFileProvider(GitFileProviderSettings args, bool allowListing)
        {
            this.allowListing = allowListing;
            this.repositoryAddress = args.Repository;
            this.branch = args.Branch;
            this.localCache = GetCacheFolder(args.LocalCache);
            this.repository = new Repository(this.repositoryAddress);
        }

        public IRadiaDirectoryContents GetDirectoryContents(string subpath)
        {
            //var directoryContents = this.radiaFileProvider.GetDirectoryContents(subpath);
            return new GitDirectoryContents();
        }

        public IRadiaFileInfo GetFileInfo(string subpath)
        {
            //var localFileInfo = this.radiaFileProvider.GetFileInfo(subpath);
            throw new NotImplementedException();
            //using (var repo = new Repository(this.localCache))
            //{
            //    localFileInfo = GetLastModifiedDate(repo, subpath, localFileInfo);
            //}
            //return localFileInfo;
        }

        private IRadiaFileInfo GetLastModifiedDate(Repository repo, string subpath, IRadiaFileInfo localFileInfo)
        {
            var status = repo.RetrieveStatus(subpath);
            var fileHistory = repo.Commits.QueryBy(subpath);

            if (fileHistory.Any() is false)
                return localFileInfo;

            return localFileInfo;            
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
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
                LibGit2Sharp.PullOptions options = new LibGit2Sharp.PullOptions();
                options.FetchOptions = new FetchOptions();
                var signature = new LibGit2Sharp.Signature(new Identity("Radia", "radia@local.host"), DateTimeOffset.Now);
                Commands.Pull(repo, signature, options);
                
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GitFileProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
