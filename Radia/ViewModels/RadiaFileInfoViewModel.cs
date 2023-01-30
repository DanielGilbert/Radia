using Radia.Services;
using Radia.Services.FileProviders;
using System.IO;

namespace Radia.ViewModels
{
    public class RadiaFileInfoViewModel
    {
        private string webHost;
        private IRadiaFileInfo dir;
        private string path;
        public string Url { get; set; }
        public string Name => dir.Name;
        public long Length => dir.Length;
        public string FormattedLength { get; }
        public DateTimeOffset LastModified => dir.LastModified;

        public RadiaFileInfoViewModel(IByteSizeService byteSizeService, string webHost, IRadiaFileInfo dir, bool isDirectory, string path)
        {
            this.webHost = webHost;
            this.dir = dir;
            this.path = path;
            Url = CreateUrl(isDirectory);
            if (dir.Length >= 0)
            {
                FormattedLength = byteSizeService.Build((ulong)dir.Length);
            }
        }

        private string CreateUrl(bool isDirectory)
        {
            IList<string> components = path.Split('/').ToList();
            components.Add(dir.Name);
            components = components.Where(x => string.IsNullOrWhiteSpace(x) is false).ToList();
            string completePath = string.Join('/', components.ToArray());
            return new Uri(this.webHost + '/' + completePath + (isDirectory ? "/" : "")).ToString();
        }

    }


}
