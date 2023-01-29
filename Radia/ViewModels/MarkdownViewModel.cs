namespace Radia.ViewModels
{
    public class MarkdownViewModel : BaseViewModel
    {
        public string Content { get; set; }

        public MarkdownViewModel(string content, string pageTitle, string pageHeader, string websiteRoot) : base(pageTitle, pageHeader, websiteRoot)
        {
            Content = content;
        }
    }
}
