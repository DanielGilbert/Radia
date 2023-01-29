using Microsoft.Extensions.FileProviders;
using Radia.Models;

namespace Radia.ViewModels
{
    public class FolderViewModel : BaseViewModel
    {
        private readonly char pathDelimiter;

        public IList<IRadiaFileInfo> Directories { get; }
        public IList<IRadiaFileInfo> Files { get; }
        public IList<IRadiaAncestorInfo> Ancestors { get; }
        public IRadiaAncestorInfo? LastAncestor { get; }

        public FolderViewModel(string pageTitle,
                               string pageHeader,
                               string relativePath,
                               char pathDelimiter,
                               string websiteRoot) : base(pageTitle, pageHeader, relativePath, websiteRoot)
        {
            Directories = new List<IRadiaFileInfo>();
            Files = new List<IRadiaFileInfo>();
            this.pathDelimiter = pathDelimiter;
            Ancestors = CreateAncestorList();
        }

        private IList<IRadiaAncestorInfo> CreateAncestorList()
        {
            var ancestors = new List<IRadiaAncestorInfo>();
            var path = RelativePath;

            if (path is null)
            {
                return ancestors;
            }

            //if (this.fileInfo.IsDirectory is false
            //    && string.IsNullOrWhiteSpace(path) is false
            //    && path.Contains(this.pathDelimiter))
            //{
            //    path = path[..path.LastIndexOf(this.pathDelimiter)];
            //}

            while (path.TrimEnd(this.pathDelimiter) != String.Empty)
            {
                if (path.Contains(this.pathDelimiter))
                {
                    var fileName = path[(path.LastIndexOf(this.pathDelimiter) + 1)..];
                    ancestors.Add(new RadiaAncestorInfo(this.WebsiteRoot, path, fileName, this.pathDelimiter));
                    path = path[..path.LastIndexOf(this.pathDelimiter)];
                }
                else
                {
                    ancestors.Add(new RadiaAncestorInfo(this.WebsiteRoot, path, path, this.pathDelimiter));
                    path = string.Empty;
                }
            }
            return ancestors.Reverse<IRadiaAncestorInfo>().ToArray();
        }
    }
}
