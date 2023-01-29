using Microsoft.AspNetCore.Mvc.Localization;
using Radia.ViewModels;

namespace Radia.Factories.View
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
