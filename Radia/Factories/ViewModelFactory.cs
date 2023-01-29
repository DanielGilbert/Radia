using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.FileProviders;
using Radia.Models;
using Radia.Services;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;
using Radia.ViewModels;

namespace Radia.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IConfigurationService configurationService;
        private readonly IContentTypeIdentifierService contentTypeIdentifierService;
        private readonly IContentProcessorFactory<string> contentProcessorFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRadiaFileProvider fileProvider;

        public ViewModelFactory(IRadiaFileProviderFactory fileProviderFactory,
                                IConfigurationService configurationService,
                                IContentTypeIdentifierService contentTypeIdentifierService,
                                IContentProcessorFactory<string> contentProcessorFactory,
                                IHttpContextAccessor httpContextAccessor,
                                IFileProviderConfiguration fileProviderConfiguration)
        {
            this.configurationService = configurationService;
            this.contentTypeIdentifierService = contentTypeIdentifierService;
            this.contentProcessorFactory = contentProcessorFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.fileProvider = fileProviderFactory.Create(fileProviderConfiguration);
        }

        public IViewModel Create(ViewModelFactoryArgs args)
        {
            IContentProcessor<string> contentProcessor = this.contentProcessorFactory.Create();

            string webHost = this.httpContextAccessor.HttpContext?.Request.Scheme + "://" + this.httpContextAccessor.HttpContext?.Request.Host.ToString() ?? string.Empty;

            IFileInfo fileInfo = this.fileProvider.GetFileInfo(args.Path);

            if (fileInfo.Exists is false)
            {
                //Maybe we are dealing with a folder?
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
                            folderViewModel.Directories.Add(new RadiaFileInfo(webHost, dir, args.Path, this.fileProvider.PathDelimiter));
                        }
                        else
                        {
                            folderViewModel.Files.Add(new RadiaFileInfo(webHost, dir, args.Path, this.fileProvider.PathDelimiter));
                        }
                    }

                    return folderViewModel;
                }
            }

            var contentType = this.contentTypeIdentifierService.GetContentTypeFrom(args.Path);

            var stream = fileInfo.CreateReadStream();
            
            var contentResult = contentProcessor.ProcessContent(contentType, stream);

            return new PhysicalFileViewModel(contentResult,
                                             contentType,
                                             fileInfo.Name,
                                             configurationService.GetWebsiteTitle(),
                                             configurationService.GetPageHeader(),
                                             fileInfo.PhysicalPath ?? string.Empty);
        }
    }
}
