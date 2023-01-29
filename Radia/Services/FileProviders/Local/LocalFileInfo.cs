using Microsoft.Extensions.FileProviders;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileInfo : IRadiaFileInfo
    {
        private readonly IFileInfo fileInfo;

        public bool Exists => this.fileInfo.Exists;

        public long Length => this.fileInfo.Length;

        public string? PhysicalPath => this.fileInfo.PhysicalPath;

        public string Name => this.fileInfo.Name;

        public DateTimeOffset LastModified => this.fileInfo.LastModified;

        public bool IsDirectory => this.fileInfo.IsDirectory;

        public LocalFileInfo(IFileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }
        public Stream CreateReadStream()
        {
            return this.fileInfo.CreateReadStream();
        }
    }
}
