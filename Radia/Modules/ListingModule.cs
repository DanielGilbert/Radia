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
        private readonly ILogger<ListingModule> logger;

        public ListingModule(IViewModelFactory viewModelFactory,
                             IViewFactory viewFactory,
                             IConfigurationService configurationService,
                             IMemoryCache memoryCache,
                             ILogger<ListingModule> logger)
        {
            this.viewModelFactory = viewModelFactory;
            this.viewFactory = viewFactory;
            this.configurationService = configurationService;
            this.memoryCache = memoryCache;
            this.logger = logger;
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
            this.logger.LogDebug($"Processing {path}");

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "/";
            }

            var resultViewModel = this.memoryCache.GetOrCreate<IViewModel>(path, cacheEntry =>
            {
                var viewModelFactoryArgs = new ViewModelFactoryArgs(path,
                                                    this.configurationService.GetWebsiteTitle());
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var resultViewModel = this.viewModelFactory.Create(viewModelFactoryArgs);
                return resultViewModel;
            });

            if (resultViewModel is IDownloadableFileViewModel downloadableFileViewModel)
            {
                return Results.File(downloadableFileViewModel.FileInfo.CreateReadStream(),
                                    downloadableFileViewModel.ContentType,
                                    enableRangeProcessing: true);
            }

            IResult result = this.memoryCache.GetOrCreate<IResult>(path + "/View", cachedEntry =>
            {
                var resultView = this.viewFactory.Create(resultViewModel!);

                return Results.Extensions.View(resultView, resultViewModel);
            });

            return result;

        }
    }
}
