using Radia.Services;

namespace Radia.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        public string PageTitle { get; }
        public string PageHeader { get; }
        public string WebsiteRoot { get; }
        public string FooterContent { get; }

        public BaseViewModel(string pageTitle,
                             string pageHeader,
                             string websiteRoot,
                             IFooterService footerService)
        {
            PageTitle = pageTitle;
            PageHeader = pageHeader;
            WebsiteRoot = websiteRoot;
            FooterContent = footerService.GetFormattedFooter();

        }
    }
}
