using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileProviders;
using Radia.Factories;
using Radia.Factories.View;
using Radia.Factories.ViewModel;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.ViewModels;
using System.Text;

namespace Radia.Modules
{
    public class ListingModule : IListingModule
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IViewFactory viewFactory;
        private readonly IConfigurationService configurationService;

        public ListingModule(IViewModelFactory viewModelFactory,
                             IViewFactory viewFactory,
                             IConfigurationService configurationService)
        {
            this.viewModelFactory = viewModelFactory;
            this.viewFactory = viewFactory;
            this.configurationService = configurationService;
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

            var resultViewModel = this.viewModelFactory.Create(viewModelFactoryArgs);

            if (resultViewModel is IDownloadableFileViewModel downloadableFileViewModel)
            {
                return Results.File(downloadableFileViewModel.FileInfo.CreateReadStream(),
                                    downloadableFileViewModel.ContentType,
                                    downloadableFileViewModel.FileInfo.Name,
                                    enableRangeProcessing: true);
            }

            var resultView = this.viewFactory.Create(resultViewModel);

            return Results.Extensions.View(resultView, resultViewModel);
        }
    }
}
