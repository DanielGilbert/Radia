using Dagidirli.Modules;

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
    }
}
