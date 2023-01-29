using Microsoft.Extensions.FileProviders;

namespace Radia.Services.FileProviders.Microsoft
{
    public class MicrosoftFileInfo : IFileInfo
    {
        private readonly IRadiaFileInfo radiaFileInfo;

        public MicrosoftFileInfo(IRadiaFileInfo radiaFileInfo)
        {
            this.radiaFileInfo = radiaFileInfo;
        }

        public bool Exists => this.radiaFileInfo.Exists;

        public bool IsDirectory => this.radiaFileInfo.IsDirectory;

        public DateTimeOffset LastModified => this.radiaFileInfo.LastModified;

        public long Length => this.radiaFileInfo.Length;

        public string Name => this.radiaFileInfo.Name;

        public string? PhysicalPath => this.radiaFileInfo.PhysicalPath;

        public Stream CreateReadStream()
        {
            return this.radiaFileInfo.CreateReadStream();
        }
    }
}
