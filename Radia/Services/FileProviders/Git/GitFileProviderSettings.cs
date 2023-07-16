namespace Radia.Services.FileProviders.Git
{
    public class GitFileProviderSettings
    {
        public string Repository { get; }
        public string Branch { get; }
        public string LocalCache { get; }
        public bool AllowListing { get; }

        public GitFileProviderSettings(string repository,
                                       string branch,
                                       string localCache,
                                       bool allowListing)
        {
            Repository = repository;
            Branch = branch;
            LocalCache = localCache;
            AllowListing = allowListing;
        }
    }
}
