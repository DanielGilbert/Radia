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
            PageTitle = CreatePageTitle(pageTitle, pageHeader);
            PageHeader = pageHeader;
            WebsiteRoot = websiteRoot;
            FooterContent = footerService.GetFormattedFooter();

        }

        private string CreatePageTitle(string pageTitle, string pageHeader)
        {
            return String.IsNullOrWhiteSpace(pageHeader) ? pageTitle : $"{pageHeader} - {pageTitle}";
        }
    }
}
