using Microsoft.Extensions.FileProviders;
using Radia.Models;
using Radia.Services;
using System.IO;
using System.Reflection;

namespace Radia.ViewModels
{
    public class FolderViewModel : BaseViewModel
    {
        private readonly string relativePath;

        public IList<RadiaFileInfoViewModel> Directories { get; }
        public IList<RadiaFileInfoViewModel> Files { get; }
        public IList<IRadiaAncestorInfo> Ancestors { get; }
        public IList<IRadiaAncestorInfo> AncestorsWithoutLastElement => Ancestors.Take(Ancestors.Count - 1).ToList();
        public IRadiaAncestorInfo? LastAncestor { get; }
        public string ReadmeContent { get; set; }

        public FolderViewModel(string pageTitle,
                               string pageHeader,
                               string relativePath,
                               string websiteRoot,
                               IFooterService footerService) : base(pageTitle, pageHeader, websiteRoot, footerService)
        {
            Directories = new List<RadiaFileInfoViewModel>();
            Files = new List<RadiaFileInfoViewModel>();
            this.relativePath = relativePath;
            Ancestors = CreateAncestorList();
            LastAncestor = Ancestors.LastOrDefault();
            ReadmeContent = string.Empty;
        }

        private IList<IRadiaAncestorInfo> CreateAncestorList()
        {
            var ancestors = new List<IRadiaAncestorInfo>();
            var path = this.relativePath;

            if (path is null)
            {
                return ancestors;
            }

            IList<string> components = path.Split('/').ToList();

            components = components.Where(x => string.IsNullOrWhiteSpace(x) is false).ToList();

            if (components.Any() is false)
            {
                return ancestors;
            }

            while (components.Any())
            {
                string fileName = components.Last();
                string completePath = string.Join('/', components.ToArray()) + '/';
                ancestors.Add(new RadiaAncestorInfo(new Uri(this.WebsiteRoot + '/' + completePath).ToString(), completePath, fileName));
                fileName = components.Last();
                components.RemoveAt(components.Count - 1);
            }
            return ancestors.Reverse<IRadiaAncestorInfo>().ToArray();
        }
    }
}
