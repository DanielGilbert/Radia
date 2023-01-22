using Dagidirli.Modules;
using Dagidirli.Services.FileProviders;

namespace Dagidirli.Extensions.Microsoft.AspNetCore.Builders
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddDagidirliModules(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IStatisticsModule, StatisticsModule>();
            builder.Services.AddSingleton<IListingModule, ListingModule>();

            return builder;
        }

        public static WebApplicationBuilder AddLocalServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IFileProviderFactory, FileProviderFactory>();

            return builder;
        }
    }
}
