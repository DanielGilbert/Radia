﻿using Radia.Factories.ContentProcessor;
using Radia.Factories.FileProvider;
using Radia.Factories.View;
using Radia.Factories.ViewModel;
using Radia.Modules;
using Radia.Services;
using Radia.Services.Background;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Microsoft;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Radia.Extensions.Microsoft.AspNetCore.Builders
{
    [ExcludeFromCodeCoverage]
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddRadiaModules(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IStatisticsModule, StatisticsModule>();
            builder.Services.AddSingleton<IListingModule, ListingModule>();

            return builder;
        }

        public static WebApplicationBuilder AddLocalServices(this WebApplicationBuilder builder)
        {
            var configurationService = new ConfigurationService(builder.Configuration);
            var configurations = configurationService.GetFileProviderConfigurations();

            builder.Services.AddSingleton<IConfigurationService>(configurationService);
            var radiaFileProvider = GetCompositeRadiaFileProvider(configurations);
            builder.Services.AddFluid(config =>
            {
                config.PartialsFileProvider = new MicrosoftFileProvider(radiaFileProvider);
                config.ViewsFileProvider = new MicrosoftFileProvider(radiaFileProvider);
            });
            builder.Services.AddSingleton<IRadiaFileProvider>(radiaFileProvider);
            if (radiaFileProvider is IRadiaNetworkFileProvider radiaNetworkFileProvider)
            {
                builder.Services.AddSingleton<IRadiaNetworkFileProvider>(radiaNetworkFileProvider);
            }
            builder.Services.AddHostedService<NetworkFetchBackgroundService>();
            builder.Services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            builder.Services.AddSingleton<IViewFactory, ViewFactory>();
            builder.Services.AddSingleton<IContentProcessorFactory, ContentProcessorFactory>();
            builder.Services.AddSingleton<IContentTypeIdentifierService, ContentTypeIdentifierService>();
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            string version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? String.Empty;
            builder.Services.AddSingleton<IVersionService, VersionService>(provider => new VersionService(version));
            builder.Services.AddSingleton<IFooterService, FooterService>();
            builder.Services.AddSingleton<IByteSizeService, ByteSizeService>();

            return builder;
        }

        private static IRadiaFileProvider GetCompositeRadiaFileProvider(IList<FileProviderConfiguration> configurations)
        {
            var fileProviderFactory = new FileProviderFactory();
            IRadiaFileProvider compositeRadiaFileProvider = fileProviderFactory.CreateComposite(configurations);
            return compositeRadiaFileProvider;
        }
    }
}
