namespace Radia.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        public string Content { get; }
        public string PageTitle { get; }
        public string PageHeader { get; }
        public string FullFilePath { get; }
        public string WebsiteRoot { get; }

        public BaseViewModel(string pageTitle,
                             string pageHeader,
                             string fullFilePath,
                             string websiteRoot)
        {
            Content = string.Empty;
            PageTitle = pageTitle;
            PageHeader = pageHeader;
            FullFilePath = fullFilePath;
            WebsiteRoot = websiteRoot;
        }
    }
}
