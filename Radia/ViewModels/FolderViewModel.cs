using Microsoft.Extensions.FileProviders;

namespace Radia.ViewModels
{
    public class FolderViewModel : BaseViewModel
    {
        public IList<IFileInfo> Directories { get; }
        public IList<IFileInfo> Files { get; }

        public FolderViewModel(string pageTitle,
                               string pageHeader,
                               string fullFilePath,
                               string websiteRoot) : base(pageTitle, pageHeader, fullFilePath, websiteRoot)
        {
            Directories = new List<IFileInfo>();
            Files = new List<IFileInfo>();
        }
    }
}
