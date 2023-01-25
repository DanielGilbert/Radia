using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileProviders;
using Radia.Factories;
using Radia.Services;
using Radia.ViewModels;
using System.Text;

namespace Radia.Modules
{
    public class ListingModule : IListingModule
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IViewModelFactory viewModelFactory;
        private readonly IViewFactory viewFactory;
        private readonly IConfigurationService configurationService;
        private readonly IFileProvider fileProvider;

        public ListingModule(IWebHostEnvironment webHostEnvironment,
                             IFileProviderFactory fileProviderFactory,
                             IViewModelFactory viewModelFactory,
                             IViewFactory viewFactory,
                             IConfigurationService configurationService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.viewModelFactory = viewModelFactory;
            this.viewFactory = viewFactory;
            this.configurationService = configurationService;
            var fileProviderConfiguration = this.configurationService.GetFileProviderConfiguration();
            this.fileProvider = fileProviderFactory.Create(fileProviderConfiguration);
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
            var viewModelFactoryArgs = new ViewModelFactoryArgs(path,
                                                                this.configurationService.GetPageTitle());

            var resultViewModel = this.viewModelFactory.Create(viewModelFactoryArgs);
            var resultView = this.viewFactory.Create(resultViewModel);

            return Results.Extensions.View(resultView, resultViewModel);
        }
    }
}
