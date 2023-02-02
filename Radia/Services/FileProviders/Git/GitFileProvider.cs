using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileProvider : IRadiaFileProvider
    {
        private readonly string repository;
        private readonly string branch;
        private readonly string localCache;
        private readonly IRadiaFileProvider localFileProvider;

        public GitFileProvider(GitFileProviderSettings args,
                               IRadiaFileProvider localFileProvider)
        {
            this.repository = args.Repository;
            this.branch = args.Branch;
            this.localCache = args.LocalCache;
            this.localFileProvider = localFileProvider;
        }

        public IRadiaDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IRadiaFileInfo GetFileInfo(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
