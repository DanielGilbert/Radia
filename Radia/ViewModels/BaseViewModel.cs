namespace Radia.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        public string PageTitle { get; }
        public string PageHeader { get; }
        public string WebsiteRoot { get; }
        public string CopyrightFooter { get; }

        public BaseViewModel(string pageTitle,
                             string pageHeader,
                             string websiteRoot,
                             string copyrightFooter = "")
        {
            PageTitle = pageTitle;
            PageHeader = pageHeader;
            WebsiteRoot = websiteRoot;
            CopyrightFooter = copyrightFooter;
        }
    }
}
