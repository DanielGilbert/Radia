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
                                                                this.configurationService.GetWebsiteTitle());

            var resultViewModel = this.viewModelFactory.Create(viewModelFactoryArgs);

            if (resultViewModel is IPhysicalFileViewModel physicalFileViewModel)
            {
                if (string.IsNullOrWhiteSpace(physicalFileViewModel.ContentResult.Result) is false)
                {
                    return Results.Text(physicalFileViewModel.ContentResult.Result, physicalFileViewModel.ContentType);
                }

                return Results.File(physicalFileViewModel.ContentResult.Stream,
                                    physicalFileViewModel.ContentType,
                                    physicalFileViewModel.FileName);
            }

            var resultView = this.viewFactory.Create(resultViewModel);

            return Results.Extensions.View(resultView, resultViewModel);
        }
    }
}
