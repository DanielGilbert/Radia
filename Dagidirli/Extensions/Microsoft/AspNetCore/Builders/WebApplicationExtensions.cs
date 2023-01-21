using Dagidirli.Modules;

namespace Dagidirli.Extensions.Microsoft.AspNetCore.Builders
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapDagidirliModules(this WebApplication app)
        {
            IStatisticsModule statisticsModule = app.Services.GetService<IStatisticsModule>() ?? throw new InvalidOperationException("StatisticsModule cannot be fetched.");
            IListingModule listingModule = app.Services.GetService<IListingModule>() ?? throw new InvalidOperationException("ListingModule cannot be fetched.");

            app.MapGet("/stats", () => statisticsModule.ProcessRequest());
            app.MapGet("/{*path}", (string? path) =>
            {
                if (path is not null)
                {
                    return listingModule.ProcessRequest(path);
                }
                else
                {
                    return listingModule.ProcessRequest();
                }
            });

            return app;
        }
    }
}
