using Radia.Services;

namespace Radia.ViewModels
{
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
