using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;

namespace Radia.ViewModels
{
    public class ProcessedFileViewModel : BaseViewModel, IProcessedFileViewModel
    {
        public string Content { get; }
        public string ContentType { get; set; }
        public IRadiaFileInfo FileInfo { get; set; }
        public ProcessedFileViewModel(string content,
                                      string contentType,
                                      IRadiaFileInfo fileInfo,
                                      string pageTitle,
                                      string pageHeader,
                                      string websiteRoot) : base(pageTitle, pageHeader, websiteRoot)
        {
            Content = content;
            ContentType = contentType;
            FileInfo = fileInfo;
        }
    }
}
