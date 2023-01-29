using Microsoft.Extensions.FileProviders;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;

namespace Radia.ViewModels
{
    public class DownloadableFileViewModel : BaseViewModel, IDownloadableFileViewModel
    {
        public string ContentType { get; }
        public IRadiaFileInfo FileInfo { get; }

        public DownloadableFileViewModel(IRadiaFileInfo fileInfo,
                                         string contentType,
                                         string pageTitle,
                                         string pageHeader) : base(pageTitle, pageHeader, string.Empty)
        {
            FileInfo = fileInfo;
            ContentType = contentType;
        }
    }
}
