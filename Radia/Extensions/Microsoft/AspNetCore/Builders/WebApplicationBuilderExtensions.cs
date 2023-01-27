﻿using Microsoft.Extensions.FileProviders;
using Radia.Factories;
using Radia.Modules;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Git;
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
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            builder.Services.AddSingleton<IRadiaFileProvider, GitFileProvider>();
            builder.Services.AddSingleton<IRadiaFileProvider, LocalFileProvider>();
            builder.Services.AddSingleton<IRadiaFileProviderFactory, FileProviderFactory>();
            builder.Services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            builder.Services.AddSingleton<IViewFactory, ViewFactory>();
            builder.Services.AddSingleton<IContentProcessorFactory<string>, ContentProcessorFactory>();
            builder.Services.AddSingleton<IContentTypeIdentifierService, ContentTypeIdentifierService>();

            return builder;
        }
    }
}
