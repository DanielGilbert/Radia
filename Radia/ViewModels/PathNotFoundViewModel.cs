using Radia.Services;
using System.Diagnostics.CodeAnalysis;

namespace Radia.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class PathNotFoundViewModel : BaseViewModel
    {
        public PathNotFoundViewModel(string pageTitle,
                                     string pageHeader,
                                     string websiteRoot,
                                     IFooterService footerService) : base(pageTitle, pageHeader, websiteRoot, footerService)
        {
        }
    }
}
