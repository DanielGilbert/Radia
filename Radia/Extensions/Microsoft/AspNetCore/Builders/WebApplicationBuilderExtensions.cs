using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Radia.Factories;
using Radia.Modules;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;
using System.Diagnostics.CodeAnalysis;

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
            builder.Services.AddSingleton<IConfigurationService>(configurationService);
            builder.Services.AddSingleton<IFileProviderConfiguration>(configurationService.GetFileProviderConfiguration());
            builder.Services.AddSingleton<IRadiaFileProvider, LocalFileProvider>();
            builder.Services.AddSingleton<IRadiaFileProvider, EmptyFileProvider>();
            builder.Services.AddSingleton<IRadiaFileProviderFactory, FileProviderFactory>();
            builder.Services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            builder.Services.AddSingleton<IViewFactory, ViewFactory>();
            builder.Services.AddSingleton<IContentProcessorFactory<string>, ContentProcessorFactory>();
            builder.Services.AddSingleton<IContentTypeIdentifierService, ContentTypeIdentifierService>();

            return builder;
        }
    }
}
