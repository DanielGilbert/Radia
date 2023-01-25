using Radia.Services.ContentProcessors;

namespace Radia.ViewModels
{
    public class PhysicalFileViewModel : BaseViewModel, IPhysicalFileViewModel
    {
        public IContentResult<string> ContentResult { get; }
        public string ContentType { get; }
        public string FileName { get; }

        public PhysicalFileViewModel(IContentResult<string> contentResult,
                                     string contentType,
                                     string fileName,
                                     string pageTitle,
                                     string pageHeader,
                                     string fullFilePath) : base(pageTitle, pageHeader, fullFilePath, string.Empty)
        {
            ContentResult = contentResult;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}
