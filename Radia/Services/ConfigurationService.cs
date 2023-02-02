using Radia.Models;
using Radia.Services.FileProviders;
using System.Diagnostics.CodeAnalysis;

namespace Radia.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AppConfiguration appConfiguration;

        public ConfigurationService(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            IConfigurationSection configurationSection = configuration.GetSection("AppConfiguration");

            if (configurationSection.Exists() is false)
            {
                throw new InvalidOperationException("AppConfiguration must not be empty. Check appsettings.json");
            }

            this.appConfiguration = configurationSection.Get<AppConfiguration>()
                                    ?? throw new InvalidOperationException("AppConfiguration must not be missing. Check appsettings.json");
        }

        public string GetWebsiteTitle()
        {
            ArgumentException.ThrowIfNullOrEmpty(this.appConfiguration.WebsiteTitle,
                                                     nameof(this.appConfiguration.WebsiteTitle));
            return this.appConfiguration.WebsiteTitle;
        }

        public IList<FileProviderConfiguration> GetFileProviderConfigurations()
        {
            ArgumentNullException.ThrowIfNull(appConfiguration.FileProviderConfigurations,
                                              nameof(appConfiguration.FileProviderConfigurations));
            return appConfiguration
                   .FileProviderConfigurations.OrderBy(kvp => Convert.ToInt32(kvp.Key))
                                              .Select(kvp => kvp.Value)
                                              .ToList();
        }

        public string GetPageHeader()
        {
            ArgumentException.ThrowIfNullOrEmpty(this.appConfiguration.DefaultPageHeader,
                                                 nameof(this.appConfiguration.DefaultPageHeader));
            return this.appConfiguration.DefaultPageHeader;
        }

        public string GetFooterCopyright()
        {
            return this.appConfiguration.FooterCopyright ?? String.Empty;
        }
    }
}
