using Microsoft.Extensions.FileProviders;

namespace Radia.Models
{
    public class RadiaFileInfo : IRadiaFileInfo
    {
        private readonly IFileInfo fileInfo;

        public IRadiaFileInfo Ancestor => throw new NotImplementedException();

        public bool Exists => this.fileInfo.Exists;

        public bool IsDirectory => this.fileInfo.IsDirectory;

        public DateTimeOffset LastModified => this.fileInfo.LastModified;

        public long Length => this.fileInfo.Length;

        public string Name => this.fileInfo.Name;

        public string? PhysicalPath => this.fileInfo.PhysicalPath;

        public bool IsRoot => throw new NotImplementedException();

        public RadiaFileInfo(IFileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public Stream CreateReadStream() => this.fileInfo.CreateReadStream();


    }
}
