using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Radia.Services.FileProviders.Local;
using System.IO;
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
            this.localCache = GetCacheFolder(args.Repository + args.Branch);
            this.repository = GetRepositoryInstance(this.repositoryAddress, this.branch, this.localCache);
        }

        private Repository GetRepositoryInstance(string repositoryAddress, string branch, string localCache)
        {
            Repository repository;

            try
            {
                string path = Repository.Clone(repositoryAddress, localCache, new CloneOptions()
                {
                    IsBare = false,
                    BranchName = branch
                });

                repository = new(path);
            }
            catch(NameConflictException)
            {
                if (Repository.IsValid(localCache) is false)
                    throw new InvalidOperationException("Path is not empty and not a valid repository");

                repository = new(this.localCache);
                PullOptions options = new PullOptions();
                options.MergeOptions = new MergeOptions();
                options.FetchOptions = new FetchOptions();

                Commands.Pull(repository, new Signature("Radía", "radia@local.host", new DateTimeOffset(DateTime.Now)), options);
            }



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
            return new GitDirectoryContents(this.repository, this.branch, subpath);
        }

        public IRadiaFileInfo GetFileInfo(string subpath)
        {
            return GitFileInfo.Create(this.repository, subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }

        public static String GetCacheFolder(string value)
        {
            using (System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create())
            {
                return Path.Combine(Path.GetTempPath() + $"gitTmp/{String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")))}");
            }
        }

        public void Fetch()
        {
            PullOptions options = new PullOptions();
            options.MergeOptions = new MergeOptions();
            options.FetchOptions = new FetchOptions();

            Commands.Pull(this.repository, new Signature("Radía", "radia@local.host", new DateTimeOffset(DateTime.Now)), options);
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
