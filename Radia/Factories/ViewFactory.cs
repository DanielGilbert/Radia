using Microsoft.AspNetCore.Mvc.Localization;
using Radia.ViewModels;

namespace Radia.Factories
{
    public class ViewFactory : IViewFactory
    {
        public string Create(IViewModel viewModel) =>
            viewModel switch
            {
                FolderViewModel => "FolderView",
                PathNotFoundViewModel => "PathNotFoundView",
                PhysicalFileViewModel => "PhysicalFileView",
                _ => string.Empty
            };
        
    }
}
