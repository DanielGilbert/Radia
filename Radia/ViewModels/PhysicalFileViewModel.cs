using Microsoft.Extensions.FileProviders;
using Radia.Services.ContentProcessors;

namespace Radia.ViewModels
{
    public class PhysicalFileViewModel : BaseViewModel, IPhysicalFileViewModel
    {
        public IContentResult ContentResult { get; }
        public string ContentType { get; }
        public string FileName { get; }

        public IFileInfo FileInfo { get; }

        public PhysicalFileViewModel(IFileInfo fileInfo,
                                     IContentResult contentResult,
                                     string contentType,
                                     string fileName,
                                     string pageTitle,
                                     string pageHeader,
                                     string fullFilePath) : base(pageTitle, pageHeader, fullFilePath, string.Empty)
        {
            FileInfo = fileInfo;
            ContentResult = contentResult;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}
