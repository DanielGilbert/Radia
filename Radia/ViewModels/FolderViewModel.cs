using Microsoft.Extensions.FileProviders;
using Radia.Models;
using System.IO;
using System.Reflection;

namespace Radia.ViewModels
{
    public class FolderViewModel : BaseViewModel
    {
        private readonly char pathDelimiter;

        public IList<RadiaFileInfoViewModel> Directories { get; }
        public IList<RadiaFileInfoViewModel> Files { get; }
        public IList<IRadiaAncestorInfo> Ancestors { get; }
        public IList<IRadiaAncestorInfo> AncestorsWithoutLastElement => Ancestors.Take(Ancestors.Count - 1).ToList();
        public IRadiaAncestorInfo? LastAncestor { get; }
        public string ReadmeContent { get; set; }

        public FolderViewModel(string pageTitle,
                               string pageHeader,
                               string relativePath,
                               char pathDelimiter,
                               string websiteRoot) : base(pageTitle, pageHeader, relativePath, websiteRoot)
        {
            Directories = new List<RadiaFileInfoViewModel>();
            Files = new List<RadiaFileInfoViewModel>();
            this.pathDelimiter = pathDelimiter;
            Ancestors = CreateAncestorList();
            LastAncestor = Ancestors.LastOrDefault();
            ReadmeContent = string.Empty;
        }

        private IList<IRadiaAncestorInfo> CreateAncestorList()
        {
            var ancestors = new List<IRadiaAncestorInfo>();
            var path = RelativePath;

            if (path is null)
            {
                return ancestors;
            }

            IList<string> components = path.Split('/').ToList();
            //components.Add(dir.Name);
            components = components.Where(x => string.IsNullOrWhiteSpace(x) is false).ToList();
            string completePath = string.Join('/', components.ToArray());
            //return new Uri(this.webHost + '/' + completePath + (isDirectory ? "/" : "")).ToString();

            while (completePath.TrimEnd(this.pathDelimiter) != String.Empty)
            {
                if (completePath.Contains(this.pathDelimiter))
                {
                    var fileName = completePath[(completePath.LastIndexOf(this.pathDelimiter) + 1)..];
                    ancestors.Add(new RadiaAncestorInfo(this.WebsiteRoot, completePath, fileName, this.pathDelimiter));
                    completePath = completePath[..completePath.LastIndexOf(this.pathDelimiter)];
                }
                else
                {
                    ancestors.Add(new RadiaAncestorInfo(this.WebsiteRoot, completePath, completePath, this.pathDelimiter));
                    completePath = string.Empty;
                }
            }
            return ancestors.Reverse<IRadiaAncestorInfo>().ToArray();
        }
    }
}
