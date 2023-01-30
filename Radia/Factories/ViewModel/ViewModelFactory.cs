using Radia.Factories.ContentProcessor;
using Radia.Services;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;
using Radia.ViewModels;
using System.Net.Mime;

namespace Radia.Factories.ViewModel
{
    public class ViewModelFactory : IViewModelFactory
    {
        public const int MaxProcessingFileSize = 1024 * 1024 * 1;

        private readonly IConfigurationService configurationService;
        private readonly IContentTypeIdentifierService contentTypeIdentifierService;
        private readonly IContentProcessorFactory contentProcessorFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IFooterService footerService;
        private readonly IRadiaFileProvider fileProvider;

        public ViewModelFactory(IRadiaFileProvider fileProvider,
                                IConfigurationService configurationService,
                                IContentTypeIdentifierService contentTypeIdentifierService,
                                IContentProcessorFactory contentProcessorFactory,
                                IHttpContextAccessor httpContextAccessor,
                                IFooterService footerService)
        {
            this.configurationService = configurationService;
            this.contentTypeIdentifierService = contentTypeIdentifierService;
            this.contentProcessorFactory = contentProcessorFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.footerService = footerService;
            this.fileProvider = fileProvider;
        }

        public IViewModel Create(ViewModelFactoryArgs args)
        {
            IContentProcessor contentProcessor = contentProcessorFactory.Create();
            string contentType = this.contentTypeIdentifierService.GetContentTypeFrom(args.Path);
            string webHost = httpContextAccessor.HttpContext?.Request.Scheme + "://" + httpContextAccessor.HttpContext?.Request.Host.ToString() ?? string.Empty;

            IRadiaFileInfo radiaFileInfo = fileProvider.GetFileInfo(args.Path);

            if (radiaFileInfo.Exists)
            {
                return GetViewModelForExistingFile(radiaFileInfo,
                                                   contentType,
                                                   args.Path,
                                                   webHost,
                                                   contentProcessor);
            }
            else
            {
                return GetViewModelForNonExistingFile(radiaFileInfo,
                                                      args.Path,
                                                      webHost,
                                                      contentProcessor);
            }
        }

        private IViewModel GetViewModelForExistingFile(IRadiaFileInfo radiaFileInfo,
                                                       string contentType,
                                                       string path,
                                                       string webHost,
                                                       IContentProcessor contentProcessor)
        {
            if (radiaFileInfo.Length >= MaxProcessingFileSize)
            {
                return new DownloadableFileViewModel(radiaFileInfo,
                                                     contentType,
                                                     configurationService.GetWebsiteTitle(),
                                                     configurationService.GetPageHeader(),
                                                     this.footerService);
            }

            string content = string.Empty;
            using (var reader = new StreamReader(radiaFileInfo.CreateReadStream()))
            {
                content = reader.ReadToEnd();
            }
            var result = contentProcessor.ProcessContent(contentType, content);
            if (content.Equals(result) is false)
            {
                return new ProcessedFileViewModel(result,
                                                    contentType,
                                                    path,
                                                    configurationService.GetWebsiteTitle(),
                                                    configurationService.GetPageHeader(),
                                                    webHost,
                                                    this.footerService);
            }

            return new DownloadableFileViewModel(radiaFileInfo,
                                                 contentType,
                                                 configurationService.GetWebsiteTitle(),
                                                 configurationService.GetPageHeader(),
                                                 this.footerService);

        }

        private IViewModel GetViewModelForNonExistingFile(IRadiaFileInfo radiaFileInfo,
                                                          string path,
                                                          string webHost,
                                                          IContentProcessor contentProcessor)
        {
            var directory = fileProvider.GetDirectoryContents(path);

            if (directory.Exists is false)
            {
                return new PathNotFoundViewModel(configurationService.GetWebsiteTitle(),
                                                 configurationService.GetPageHeader(),
                                                 webHost,
                                                 this.footerService);
            }

            var folderViewModel = new FolderViewModel(configurationService.GetWebsiteTitle(),
                                                      configurationService.GetPageHeader(),
                                                      path,
                                                      webHost,
                                                      this.footerService);

            folderViewModel = SetupFolderViewModel(contentProcessor,
                                                   folderViewModel,
                                                   directory,
                                                   webHost,
                                                   path);

            return folderViewModel;
        }

        private FolderViewModel SetupFolderViewModel(IContentProcessor contentProcessor,
                                                     FolderViewModel folderViewModel,
                                                     IRadiaDirectoryContents directory,
                                                     string webHost,
                                                     string path)
        {
            foreach (var dir in directory)
            {
                if (dir.IsDirectory)
                {
                    folderViewModel.Directories.Add(new RadiaFileInfoViewModel(webHost, dir, dir.IsDirectory, path));
                }
                else
                {
                    if (dir.Name.ToLowerInvariant().Equals("readme.md") || dir.Name.ToLowerInvariant().Equals(".readme.md"))
                    {
                        string cntntType = "text/markdown";
                        var stringContent = string.Empty;
                        using (var reader = new StreamReader(dir.CreateReadStream()))
                        {
                            stringContent = reader.ReadToEnd();
                        }

                        var cntntResult = contentProcessor.ProcessContent(cntntType, stringContent);
                        folderViewModel.ReadmeContent = cntntResult;
                    }
                    folderViewModel.Files.Add(new RadiaFileInfoViewModel(webHost, dir, dir.IsDirectory, path));
                }
            }

            return folderViewModel;
        }
    }
}
