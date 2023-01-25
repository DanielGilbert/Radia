using Radia.Services.ContentProcessors;

namespace Radia.ViewModels
{
    public class PhysicalFileViewModel : BaseViewModel, IPhysicalFileViewModel
    {
        public IContentResult<string> ContentResult { get; }
        public PhysicalFileViewModel(IContentResult<string> contentResult, string pageTitle, string pageHeader, string fullFilePath) : base(pageTitle, pageHeader, fullFilePath)
        {
            ContentResult = contentResult;
        }
    }
}
