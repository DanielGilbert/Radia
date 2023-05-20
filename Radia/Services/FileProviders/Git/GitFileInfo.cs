namespace Radia.Services.FileProviders.Git
{
    public class GitFileInfo : IRadiaFileInfo
    {
        private readonly IRadiaFileInfo radiaFileInfo;
        private readonly DateTimeOffset lastModifiedDate;

        public bool Exists => this.radiaFileInfo.Exists;

        public long Length => this.radiaFileInfo.Length;

        public string? PhysicalPath => this.radiaFileInfo.PhysicalPath;

        public string Name => this.radiaFileInfo.Name;

        public DateTimeOffset LastModified => this.lastModifiedDate;

        public bool IsDirectory => this.radiaFileInfo.IsDirectory;

        public GitFileInfo(DateTimeOffset lastModifiedDate)
        {
            this.radiaFileInfo = null;
            this.lastModifiedDate = lastModifiedDate;
        }

        public Stream CreateReadStream()
        {
            return this.radiaFileInfo.CreateReadStream();
        }
    }
}
