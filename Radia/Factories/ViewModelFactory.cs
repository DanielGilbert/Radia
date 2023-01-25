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
        private readonly IFileProvider fileProvider;

        public ViewModelFactory(IFileProviderFactory fileProviderFactory,
                                IConfigurationService configurationService,
                                IContentTypeIdentifierService contentTypeIdentifierService,
                                IContentProcessorFactory<string> contentProcessorFactory)
        {
            this.configurationService = configurationService;
            this.contentTypeIdentifierService = contentTypeIdentifierService;
            this.contentProcessorFactory = contentProcessorFactory;
            this.fileProvider = fileProviderFactory.Create(this.configurationService.GetFileProviderConfiguration());
        }

        public IViewModel Create(ViewModelFactoryArgs args)
        {
            IContentProcessor<string> contentProcessor = this.contentProcessorFactory.Create();

            if (args.Path == string.Empty)
            {
                return new FolderViewModel(configurationService.GetPageTitle(),
                           configurationService.GetPageTitle(),
                           args.Path);
            }

            IFileInfo fileInfo = this.fileProvider.GetFileInfo(args.Path);

            if (fileInfo.Exists is false)
            {
                return new PathNotFoundViewModel(configurationService.GetPageTitle(),
                                                 configurationService.GetPageTitle(),
                                                 args.Path);
            }

            if (fileInfo.IsDirectory)
            {
                return new FolderViewModel(configurationService.GetPageTitle(),
                                           configurationService.GetPageTitle(),
                                           args.Path);
            }

            var contentType = this.contentTypeIdentifierService.GetContentTypeFrom(args.Path);

            IContentResult<string> contentResult = new EmptyContentResult();

            using (var stream = fileInfo.CreateReadStream())
            {
                contentResult = contentProcessor.ProcessContent(contentType, stream);
            }

            return new PhysicalFileViewModel(contentResult,
                                             configurationService.GetPageTitle(),
                                             configurationService.GetPageTitle(),
                                             args.Path);
        }
    }
}
