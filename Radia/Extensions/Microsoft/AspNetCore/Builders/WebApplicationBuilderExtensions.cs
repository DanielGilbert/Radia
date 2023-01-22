using Radia.Modules;
using Radia.Services;
using Radia.Services.FileProviders;

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
            builder.Services.AddSingleton<IFileProviderFactory, FileProviderFactory>();

            return builder;
        }
    }
}
