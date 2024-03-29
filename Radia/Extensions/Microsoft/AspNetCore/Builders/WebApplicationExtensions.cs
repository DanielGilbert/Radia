﻿using Radia.Modules;
using System.Diagnostics.CodeAnalysis;

namespace Radia.Extensions.Microsoft.AspNetCore.Builders
{
    [ExcludeFromCodeCoverage]
    public static class WebApplicationExtensions
    {
        public static WebApplication MapRadiaModules(this WebApplication app)
        {
            IStatisticsModule statisticsModule = app.Services.GetService<IStatisticsModule>()
                                                 ?? throw new InvalidOperationException("StatisticsModule cannot be fetched.");
            IListingModule listingModule = app.Services.GetService<IListingModule>()
                                           ?? throw new InvalidOperationException("ListingModule cannot be fetched.");

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
