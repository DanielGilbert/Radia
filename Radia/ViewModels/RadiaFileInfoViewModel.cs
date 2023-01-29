using Radia.Services.FileProviders;

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
        public DateTimeOffset LastModified => dir.LastModified;

        public RadiaFileInfoViewModel(string webHost, IRadiaFileInfo dir, string path)
        {
            this.webHost = webHost;
            this.dir = dir;
            this.path = path;
            Url = CreateUrl();
        }

        private string CreateUrl()
        {
            return new Uri(this.webHost + '/' + this.path.Replace('\\', '/').TrimStart('/').TrimEnd('/') + $"/{this.dir.Name}").ToString();
        }

    }


}
