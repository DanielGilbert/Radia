using Microsoft.Extensions.Configuration;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;

namespace Radia.Factories.FileProvider
{
    public class FileProviderFactory : IRadiaFileProviderFactory
    {
        public IRadiaFileProvider Create(FileProviderConfiguration configuration)
        {
            IRadiaFileProvider? result = null;

            if (configuration.Settings.TryGetValue("RootDirectory", out string? localRoot))
            {
                result = new LocalFileProvider(localRoot, configuration.AllowListing);
            }

            if (configuration.Settings.TryGetValue("Repository", out string? repository))
            {
                GitFileProviderSettings gitFileProviderSettings = GetGitFileProviderSettings(configuration.Settings);
                result = new GitFileProvider(gitFileProviderSettings, configuration.AllowListing);
            }

            return result ?? throw new InvalidOperationException("No FileProvider found");
        }

        private GitFileProviderSettings GetGitFileProviderSettings(Dictionary<string, string> settings)
        {
            string repository = string.Empty;
            string branch = string.Empty;
            string localCache = string.Empty;

            if (settings.TryGetValue("Repository", out string? repositorySetting))
            {
                repository = repositorySetting;
            }

            if (settings.TryGetValue("Branch", out string? branchSetting))
            {
                branch = branchSetting;
            }

            if (settings.TryGetValue("LocalCache", out string? localCacheSetting))
            {
                localCache = localCacheSetting;
            }

            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(localCache);

            return new GitFileProviderSettings(repository,
                                               branch,
                                               localCache);
        }

        public IRadiaFileProvider CreateComposite(IList<FileProviderConfiguration> configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            IList<IRadiaFileProvider> fileProviders = new List<IRadiaFileProvider>();

            foreach (var config in configuration)
            {
                fileProviders.Add(Create(config));
            }

            IRadiaFileProvider compositeRadiaFileProvider = new CompositeRadiaFileProvider(fileProviders);
            return compositeRadiaFileProvider;
        }
    }
}
