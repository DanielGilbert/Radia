using Microsoft.Extensions.Caching.Memory;
using Radia.Factories.View;
using Radia.Factories.ViewModel;
using Radia.Services;
using Radia.ViewModels;

namespace Radia.Modules
{
    public class ListingModule : IListingModule
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IViewFactory viewFactory;
        private readonly IConfigurationService configurationService;
        private readonly IMemoryCache memoryCache;

        public ListingModule(IViewModelFactory viewModelFactory,
                             IViewFactory viewFactory,
                             IConfigurationService configurationService,
                             IMemoryCache memoryCache)
        {
            this.viewModelFactory = viewModelFactory;
            this.viewFactory = viewFactory;
            this.configurationService = configurationService;
            this.memoryCache = memoryCache;
        }

        public IResult ProcessRequest(string arg)
        {
            return InternalProcessRequest(arg);
        }

        public IResult ProcessRequest()
        {
            return InternalProcessRequest(string.Empty);
        }

        private IResult InternalProcessRequest(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "/";
            }
            var viewModelFactoryArgs = new ViewModelFactoryArgs(path,
                                                                this.configurationService.GetWebsiteTitle());

            var resultViewModel = this.memoryCache.GetOrCreate<IViewModel>(path, cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(2);
                var resultViewModel = this.viewModelFactory.Create(viewModelFactoryArgs);
                return resultViewModel;
            });

            if (resultViewModel is IDownloadableFileViewModel downloadableFileViewModel)
            {
                return Results.File(downloadableFileViewModel.FileInfo.CreateReadStream(),
                                    downloadableFileViewModel.ContentType,
                                    enableRangeProcessing: true);
            }

            var resultView = this.viewFactory.Create(resultViewModel!);

            return Results.Extensions.View(resultView, resultViewModel);
        }
    }
}
