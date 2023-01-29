﻿using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.FileProviders;
using Radia.Models;
using Radia.Services;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;
using Radia.ViewModels;
using System.Net.Mime;

namespace Radia.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IConfigurationService configurationService;
        private readonly IContentTypeIdentifierService contentTypeIdentifierService;
        private readonly IContentProcessorFactory contentProcessorFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRadiaFileProvider fileProvider;

        public ViewModelFactory(IRadiaFileProvider fileProvider,
                                IConfigurationService configurationService,
                                IContentTypeIdentifierService contentTypeIdentifierService,
                                IContentProcessorFactory contentProcessorFactory,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.configurationService = configurationService;
            this.contentTypeIdentifierService = contentTypeIdentifierService;
            this.contentProcessorFactory = contentProcessorFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.fileProvider = fileProvider;
        }

        public IViewModel Create(ViewModelFactoryArgs args)
        {
            IContentProcessor contentProcessor = this.contentProcessorFactory.Create();

            string webHost = this.httpContextAccessor.HttpContext?.Request.Scheme + "://" + this.httpContextAccessor.HttpContext?.Request.Host.ToString() ?? string.Empty;

            IFileInfo fileInfo = this.fileProvider.GetFileInfo(args.Path);

            if (fileInfo.Exists is false)
            {
                var directoryContent = this.fileProvider.GetDirectoryContents(args.Path);

                if (directoryContent.Exists is false)
                {
                    return new PathNotFoundViewModel(configurationService.GetWebsiteTitle(),
                                 configurationService.GetPageHeader(),
                                 args.Path,
                                 webHost);
                }
                else
                {
                    var folderViewModel = new FolderViewModel(configurationService.GetWebsiteTitle(),
                                                          configurationService.GetPageHeader(),
                                                          args.Path,
                                                          '/',
                                                          webHost);



                    foreach (var dir in directoryContent)
                    {
                        if (dir.IsDirectory)
                        {
                            folderViewModel.Directories.Add(new RadiaFileInfo(webHost, dir, args.Path));
                        }
                        else
                        {
                            if (dir.Name.ToLowerInvariant().Equals("readme.md"))
                            {
                                string cntntType = "text/markdown";
                                var result = this.contentProcessorFactory.Create();
                                var stringContent = string.Empty;
                                using (var reader = new StreamReader(dir.CreateReadStream()))
                                {
                                    stringContent = reader.ReadToEnd();
                                }

                                var cntntResult = result.ProcessContent(cntntType, stringContent);
                                folderViewModel.ReadmeContent = cntntResult.Result;
                            }
                            folderViewModel.Files.Add(new RadiaFileInfo(webHost, dir, args.Path));
                        }
                    }

                    return folderViewModel;
                }
            }

            var contentType = this.contentTypeIdentifierService.GetContentTypeFrom(args.Path);

            var fileContentString = string.Empty;

            if (contentType.StartsWith("text/"))
            {
                using(var reader = new StreamReader(fileInfo.CreateReadStream()))
                {
                    fileContentString = reader.ReadToEnd();

                    var cntntResult = new PlainTextContentResult(fileContentString, contentType);

                    return new PhysicalFileViewModel(fileInfo,
                                 cntntResult,
                                 contentType,
                                 fileInfo.Name,
                                 configurationService.GetWebsiteTitle(),
                                 configurationService.GetPageHeader(),
                                 fileInfo.PhysicalPath ?? string.Empty);
                }
            }

            var stream = fileInfo.CreateReadStream();

            var contentResult = contentProcessor.ProcessContent(contentType, fileContentString);

            return new PhysicalFileViewModel(fileInfo,
                                             contentResult,
                                             contentType,
                                             fileInfo.Name,
                                             configurationService.GetWebsiteTitle(),
                                             configurationService.GetPageHeader(),
                                             fileInfo.PhysicalPath ?? string.Empty);
        }
    }
}
