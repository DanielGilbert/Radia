using IniParser.Model;
using IniParser;
using Radia.Factories.ContentProcessor;
using Radia.Services;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;
using Radia.ViewModels;
using IniParser.Parser;
using System.Net.Http;

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
        private readonly ILogger<ViewModelFactory> logger;
        private readonly IRadiaFileProvider fileProvider;
        private readonly IByteSizeService byteSizeService;

        public ViewModelFactory(IRadiaFileProvider fileProvider,
                                IConfigurationService configurationService,
                                IContentTypeIdentifierService contentTypeIdentifierService,
                                IContentProcessorFactory contentProcessorFactory,
                                IHttpContextAccessor httpContextAccessor,
                                IByteSizeService byteSizeService,
                                IFooterService footerService,
                                ILogger<ViewModelFactory> logger)
        {
            this.configurationService = configurationService;
            this.contentTypeIdentifierService = contentTypeIdentifierService;
            this.contentProcessorFactory = contentProcessorFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.footerService = footerService;
            this.logger = logger;
            this.fileProvider = fileProvider;
            this.byteSizeService = byteSizeService;
        }

        public IViewModel Create(ViewModelFactoryArgs args)
        {
            IContentProcessor contentProcessor = contentProcessorFactory.Create();
            string contentType = this.contentTypeIdentifierService.GetContentTypeFrom(args.Path);
            string webHost = httpContextAccessor.HttpContext?.Request.Scheme + "://" + httpContextAccessor.HttpContext?.Request.Host.ToString() ?? string.Empty;

            IRadiaFileInfo radiaFileInfo = fileProvider.GetFileInfo(args.Path);

            this.logger.LogDebug($"Matching to file object {radiaFileInfo}");

            if (radiaFileInfo.Exists)
            {
                this.logger.LogDebug("Getting ViewModel for existing file");
                return GetViewModelForExistingFile(radiaFileInfo,
                                                   contentType,
                                                   args.Path,
                                                   webHost,
                                                   contentProcessor);
            }
            else
            {
                this.logger.LogDebug("Getting ViewModel for non-existing file");
                return GetViewModelForNonExistingFile(this.byteSizeService,
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
                this.logger.LogDebug("File is too large to process, downloading instead");
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
            this.logger.LogDebug("Processing content");
            var result = contentProcessor.ProcessContent(contentType, content);
            if (content.Equals(result) is false)
            {
                this.logger.LogDebug("File got processed");
                return new ProcessedFileViewModel(result,
                                                    contentType,
                                                    path,
                                                    configurationService.GetWebsiteTitle(),
                                                    Path.GetFileName(path) ?? configurationService.GetPageHeader(),
                                                    webHost,
                                                    this.footerService);
            }

            this.logger.LogDebug("File for download");
            return new DownloadableFileViewModel(radiaFileInfo,
                                                 contentType,
                                                 configurationService.GetWebsiteTitle(),
                                                 configurationService.GetPageHeader(),
                                                 this.footerService);

        }

        private IViewModel GetViewModelForNonExistingFile(IByteSizeService byteSizeService,
                                                          string path,
                                                          string webHost,
                                                          IContentProcessor contentProcessor)
        {
            this.logger.LogDebug("Trying to get as directory");
            var directory = fileProvider.GetDirectoryContents(path);

            if (directory.Exists is false)
            {
                return new PathNotFoundViewModel(configurationService.GetWebsiteTitle(),
                                                 configurationService.GetPageHeader(),
                                                 webHost,
                                                 this.footerService);
            }

            var folderViewModel = new FolderViewModel(configurationService.GetWebsiteTitle(),
                                                      String.IsNullOrWhiteSpace(path) 
                                                      || path == "/" ? configurationService.GetPageHeader() 
                                                                     : (Path.GetDirectoryName(path) ?? configurationService.GetPageHeader()),
                                                      path,
                                                      webHost,
                                                      this.footerService);

            folderViewModel = SetupFolderViewModel(contentProcessor,
                                                   byteSizeService,
                                                   folderViewModel,
                                                   directory,
                                                   webHost,
                                                   path);

            return folderViewModel;
        }

        private FolderViewModel SetupFolderViewModel(IContentProcessor contentProcessor,
                                                     IByteSizeService byteSizeService,
                                                     FolderViewModel folderViewModel,
                                                     IRadiaDirectoryContents directory,
                                                     string webHost,
                                                     string path)
        {
            bool hasMetaFile = false;
            string metaFileContent = string.Empty;

            foreach (var dir in directory)
            {
                if (dir.IsDirectory)
                {
                    folderViewModel.Directories.Add(new RadiaFileInfoViewModel(byteSizeService, webHost, dir, dir.IsDirectory, path));
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
                    else if (dir.Name.ToLowerInvariant().Equals(".meta"))
                    {
                        using (var reader = new StreamReader(dir.CreateReadStream()))
                        {
                            metaFileContent = reader.ReadToEnd();
                            hasMetaFile = true;
                        }
                    }
                    else
                    {
                        folderViewModel.Files.Add(new RadiaFileInfoViewModel(byteSizeService, webHost, dir, dir.IsDirectory, path));
                    }
                }
            }

            if (hasMetaFile)
            {
                ParseDescriptionsFromMetaContent(folderViewModel, metaFileContent);
            }

            return folderViewModel;
        }

        private void ParseDescriptionsFromMetaContent(FolderViewModel folderViewModel, string metaFileContent)
        {
            var result = new Dictionary<string, string>();

            if (!String.IsNullOrWhiteSpace(metaFileContent))
            {
                IniDataParser iniDataParser = new();
                var data = iniDataParser.Parse(metaFileContent);

                foreach (var section in data.Sections)
                {
                    if (section.Keys.ContainsKey("Description"))
                    {
                        result.Add(section.SectionName, section.Keys["Description"]);
                        folderViewModel.HasAnyDescriptions = true;
                    }
                }
            }

            foreach(var directory in folderViewModel.Directories)
            {
                if (result.TryGetValue(directory.Name, out string? description))
                {
                    directory.Description = description ?? string.Empty;
                    folderViewModel.HasAnyDescriptions = true;
                }

            }

            foreach (var file in folderViewModel.Files)
            {
                if (result.TryGetValue(file.Name, out string? description))
                {
                    file.Description = description ?? string.Empty;
                }

            }
        }
    }
}
