namespace Radia.Services.FileProviders.Git
{
    public class GitFileProviderSettings
    {
        public string Repository { get; set; }
        public string Branch { get; set; }
        public string LocalCache { get; set; }

        public GitFileProviderSettings(string repository, string branch, string localCache)
        {
            Repository = repository;
            Branch = branch;
            LocalCache = localCache;
        }
    }
}
