using Radia.Modules;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;

namespace Radia.Extensions.Microsoft.AspNetCore.Builders
{
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
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            var localFileProvider = new LocalFileProvider();
            var gitFileProvider = new GitFileProvider();
            var fileProviderFactory = new FileProviderFactory(gitFileProvider, localFileProvider);
            builder.Services.AddSingleton<IFileProviderFactory>(fileProviderFactory);

            return builder;
        }
    }
}
