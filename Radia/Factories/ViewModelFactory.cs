using Microsoft.Extensions.FileProviders;
using Radia.Services;
using Radia.Services.ContentProcessors;
using Radia.ViewModels;

namespace Radia.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IConfigurationService configurationService;
        private readonly IContentTypeIdentifierService contentTypeIdentifierService;
        private readonly IContentProcessorFactory<string> contentProcessorFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IFileProvider fileProvider;

        public ViewModelFactory(IFileProviderFactory fileProviderFactory,
                                IConfigurationService configurationService,
                                IContentTypeIdentifierService contentTypeIdentifierService,
                                IContentProcessorFactory<string> contentProcessorFactory,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.configurationService = configurationService;
            this.contentTypeIdentifierService = contentTypeIdentifierService;
            this.contentProcessorFactory = contentProcessorFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.fileProvider = fileProviderFactory.Create(this.configurationService.GetFileProviderConfiguration());
        }

        public IViewModel Create(ViewModelFactoryArgs args)
        {
            IContentProcessor<string> contentProcessor = this.contentProcessorFactory.Create();

            string websiteRoot = this.httpContextAccessor.HttpContext?.Request.Scheme + "://" + this.httpContextAccessor.HttpContext?.Request.Host.ToString() ?? string.Empty;

            if (args.Path == string.Empty)
            {
                return new FolderViewModel(configurationService.GetWebsiteTitle(),
                                           configurationService.GetPageHeader(),
                                           args.Path,
                                           websiteRoot);
            }

            IFileInfo fileInfo = this.fileProvider.GetFileInfo(args.Path);

            if (fileInfo.Exists is false)
            {
                return new PathNotFoundViewModel(configurationService.GetWebsiteTitle(),
                                                 configurationService.GetPageHeader(),
                                                 args.Path,
                                                 websiteRoot);
            }

            if (fileInfo.IsDirectory)
            {
                return new FolderViewModel(configurationService.GetWebsiteTitle(),
                                           configurationService.GetPageHeader(),
                                           args.Path,
                                           websiteRoot);
            }

            var contentType = this.contentTypeIdentifierService.GetContentTypeFrom(args.Path);

            IContentResult<string> contentResult = new EmptyContentResult();

            var stream = fileInfo.CreateReadStream();
            
            contentResult = contentProcessor.ProcessContent(contentType, stream);

            return new PhysicalFileViewModel(contentResult,
                                             contentType,
                                             fileInfo.Name,
                                             configurationService.GetWebsiteTitle(),
                                             configurationService.GetPageHeader(),
                                             fileInfo.PhysicalPath ?? string.Empty);
        }
    }
}
