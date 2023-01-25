﻿using Radia.Models;
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

            this.appConfiguration = configuration?.GetSection("AppConfiguration").Get<AppConfiguration>()
                                    ?? throw new InvalidOperationException("AppConfiguration must not be null. Check appsettings.json");
        }

        public string GetWebsiteTitle()
        {
            ArgumentNullException.ThrowIfNullOrEmpty(this.appConfiguration.WebsiteTitle,
                                                     nameof(this.appConfiguration.WebsiteTitle));
            return this.appConfiguration.WebsiteTitle;
        }

        public FileProviderConfiguration GetFileProviderConfiguration()
        {
            ArgumentNullException.ThrowIfNull(appConfiguration.FileProviderConfiguration,
                                              nameof(appConfiguration.FileProviderConfiguration));
            return appConfiguration.FileProviderConfiguration;
        }

        public string GetPageHeader()
        {
            ArgumentNullException.ThrowIfNullOrEmpty(this.appConfiguration.DefaultPageHeader,
                                                     nameof(this.appConfiguration.DefaultPageHeader));
            return this.appConfiguration.DefaultPageHeader;
        }
    }
}