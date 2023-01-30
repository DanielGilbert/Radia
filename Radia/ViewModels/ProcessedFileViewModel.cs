using Radia.Models;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;

namespace Radia.ViewModels
{
    public class ProcessedFileViewModel : BaseViewModel, IProcessedFileViewModel
    {
        private readonly string relativePath;

        public string Content { get; }
        public string ContentType { get; set; }
        public IList<IRadiaAncestorInfo> Ancestors { get; }
        public IRadiaAncestorInfo? LastAncestor { get; }
        public IList<IRadiaAncestorInfo> AncestorsWithoutLastElement => Ancestors.Take(Ancestors.Count - 1).ToList();

        public ProcessedFileViewModel(string content,
                                      string contentType,
                                      string relativePath,
                                      string pageTitle,
                                      string pageHeader,
                                      string websiteRoot) : base(pageTitle, pageHeader, websiteRoot)
        {
            Content = content;
            ContentType = contentType;
            this.relativePath = relativePath;
            Ancestors = CreateAncestorList();
            LastAncestor = Ancestors.LastOrDefault();
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

            int i = 0;

            while (components.Any())
            {
                string fileName = components.Last();
                string completePath = string.Empty;
                if (i > 0)
                {
                    completePath = string.Join('/', components.ToArray()) + '/';
                }
                else
                {
                    completePath = string.Join('/', components.ToArray());
                }
                ancestors.Add(new RadiaAncestorInfo(new Uri(this.WebsiteRoot + '/' + completePath).ToString(), completePath, fileName));
                components.RemoveAt(components.Count - 1);
                i++;
            }
            return ancestors.Reverse<IRadiaAncestorInfo>().ToArray();
        }
    }
}
