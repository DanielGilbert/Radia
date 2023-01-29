using Microsoft.Extensions.FileProviders;
using Microsoft.VisualBasic;
using System.IO;

namespace Radia.Models
{
    public class RadiaFileInfo : IRadiaFileInfo
    {
        private readonly string webHost;
        private readonly IFileInfo fileInfo;
        private readonly string relativePath;
        private readonly char pathDelimiter;

        public bool Exists => this.fileInfo.Exists;

        public bool IsDirectory => this.fileInfo.IsDirectory;

        public DateTimeOffset LastModified => this.fileInfo.LastModified;

        public long Length => this.fileInfo.Length;

        public string Name => this.fileInfo.Name;

        public string? PhysicalPath => this.fileInfo.PhysicalPath;

        public bool IsRoot => throw new NotImplementedException();

        public string Url { get; }

        public RadiaFileInfo(string webHost,
                             IFileInfo fileInfo,
                             string relativePath,
                             char pathDelimiter)
        {
            this.webHost = webHost;
            this.fileInfo = fileInfo;
            this.relativePath = relativePath;
            this.pathDelimiter = pathDelimiter;
            Url = CreateUrl();
        }

        private string CreateUrl()
        {
            return new Uri(Uri.EscapeUriString(this.webHost + '/' + this.relativePath.Replace(this.pathDelimiter, '/').TrimStart('/').TrimEnd('/') + $"/{this.fileInfo.Name}")).ToString();
        }

        public Stream CreateReadStream() => this.fileInfo.CreateReadStream();


    }
}
