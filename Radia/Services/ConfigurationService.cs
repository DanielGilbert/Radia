using Radia.Models;
using Radia.Services.FileProviders;
using System.Diagnostics.CodeAnalysis;

namespace Radia.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private AppConfiguration appConfiguration;

        public ConfigurationService(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.appConfiguration = configuration?.GetSection("AppConfiguration").Get<AppConfiguration>()
                                    ?? throw new InvalidOperationException("AppConfiguration must not be null. Check appsettings.json");

            if (this.appConfiguration.FileProviderConfiguration is null)
            {
                throw new InvalidOperationException("FileProviderConfiguration must not be null. Check appsettings.json");
            }

            //                            ?? throw new InvalidOperationException("RootDirectory must not be null. Check appsettings.json");
            //string websiteTitle = configuration?.GetSection("AppConfiguration")["WebsiteTitle"]
            //                            ?? throw new InvalidOperationException("WebsiteTitle must not be null. Check appsettings.json");
            //string webpageTitle = configuration?.GetSection("AppConfiguration")["WebsiteH1Title"]
            //                            ?? throw new InvalidOperationException("WebsiteH1Title must not be null. Check appsettings.json");

            //string rootDirectory = Path.GetFullPath(Path.Combine(rootDirectoryValue, string.Empty));
        }

        //public string GetRootDirectory()
        //{
        //    return directoryListingConfiguration.RootDirectory;
        //}

        public FileProviderConfiguration GetFileProviderConfiguration()
        {
            if (this.appConfiguration.FileProviderConfiguration is null)
            {
                throw new InvalidOperationException("FileProviderConfiguration must not be null. Check appsettings.json");
            }

            return appConfiguration.FileProviderConfiguration;
        }
    }
}
