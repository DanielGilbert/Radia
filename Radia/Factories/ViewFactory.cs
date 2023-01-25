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
                PathNotFoundViewModel => "PathNotFound",
                PhysicalFileViewModel => "PhysicalFileViewModel",
                _ => string.Empty
            };
        
    }
}
