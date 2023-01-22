using Microsoft.Extensions.FileProviders;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.ViewModels;
using System.Text;

namespace Radia.Modules
{
    public class ListingModule : IListingModule
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFileProvider fileProvider;

        public ListingModule(IWebHostEnvironment webHostEnvironment,
                             IFileProviderFactory fileProviderFactory,
                             IConfigurationService configurationService)
        {
            this.webHostEnvironment = webHostEnvironment;

            var fileProviderConfiguration = configurationService.GetFileProviderConfiguration();
            this.fileProvider = fileProviderFactory.Create(fileProviderConfiguration);
        }

        public IResult ProcessRequest(string arg)
        {
            var file = Path.Combine(this.webHostEnvironment.WebRootPath, arg);
            
            return Results.Text(File.ReadAllText(file, Encoding.UTF8), "text/css", Encoding.UTF8);
        }

        public IResult ProcessRequest()
        {
            return Results.Extensions.View("FolderView", new FolderViewModel());
        }
    }
}
