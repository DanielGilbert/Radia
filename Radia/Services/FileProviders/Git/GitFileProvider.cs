using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Radia.Services.FileProviders.Local;
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

        /// <summary>
        /// This file provider implementation works on git branches.
        /// </summary>
        /// <exception cref="BranchNotFoundException">Gets thrown if the given branch cannot be found.</exception>
        /// <param name="args">A set of arguments for the GitFileProvider</param>
        /// <param name="allowListing"></param>
        public GitFileProvider(GitFileProviderSettings args)
        {
            this.allowListing = args.AllowListing;
            this.repositoryAddress = args.Repository;
            this.branch = args.Branch;
            this.localCache = GetCacheFolder(args.LocalCache);
            this.repository = GetRepositoryInstance(this.repositoryAddress, this.branch);
        }

        private Repository GetRepositoryInstance(string repositoryAddress, string branch)
        {
            Repository repository = new(repositoryAddress);
            if (repository.Refs[$"refs/heads/{branch}"] is Reference reference)
            {
                repository.Refs.UpdateTarget(repository.Refs.Head, reference);
            }
            else
            {
                throw new BranchNotFoundException(branch);
            }
            return repository;
        }

        public IRadiaDirectoryContents GetDirectoryContents(string subpath)
        {
            //var directoryContents = this.radiaFileProvider.GetDirectoryContents(subpath);
            return new GitDirectoryContents(this.repository, this.branch, subpath);
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

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }

        public static String GetCacheFolder(string value)
        {
            using (System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create())
            {
                return $"/gitTmp/{String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")))}";
            }
        }

        public void Fetch()
        {
            //var remote = this.repository.Network.Remotes["origin"];
            //var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
            //Commands.Fetch(this.repository, remote.Name, refSpecs, null, string.Empty);
            //PullOptions options = new PullOptions();
            //options.FetchOptions = new FetchOptions();
            //var signature = new Signature(new Identity("Radia", "radia@local.host"), DateTimeOffset.Now);
            //Commands.Pull(this.repository, signature, options);
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
