using LibGit2Sharp;

namespace Radia.Services.FileProviders.Git
{
    public class GitDirectoryInfo : IRadiaFileInfo
    {
        private readonly TreeEntry treeEntry;

        public bool Exists => true;

        public long Length => 0;

        public string? PhysicalPath => this.treeEntry.Path;

        public string Name => this.treeEntry.Name;

        public DateTimeOffset LastModified { get; }

        public bool IsDirectory => true;

        private GitDirectoryInfo(TreeEntry treeEntry,
                                 DateTimeOffset dateTimeOffset)
        {
            this.treeEntry = treeEntry;
            this.LastModified = dateTimeOffset;
        }

        public Stream CreateReadStream()
        {
            throw new InvalidOperationException("Cannot create stream from directory.");
        }

        public static IRadiaFileInfo Create(TreeEntry entry, DateTimeOffset lastModifiedDate)
        {
            return new GitDirectoryInfo(entry, lastModifiedDate);
        }
    }
}
