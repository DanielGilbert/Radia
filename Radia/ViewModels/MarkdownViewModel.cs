using Radia.Services;
using System.Diagnostics.CodeAnalysis;

namespace Radia.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class MarkdownViewModel : BaseViewModel
    {
        public string Content { get; set; }

        public MarkdownViewModel(string content,
                                 string pageTitle,
                                 string pageHeader,
                                 string websiteRoot,
                                 IFooterService footerService) : base(pageTitle, pageHeader, websiteRoot, footerService)
        {
            Content = content;
        }
    }
}
