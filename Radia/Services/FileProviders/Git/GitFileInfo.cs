using LibGit2Sharp;
using System.Runtime.CompilerServices;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileInfo : IRadiaFileInfo
    {
        private readonly TreeEntry treeEntry;
        private readonly DateTimeOffset lastModifiedDate;

        public bool Exists => true;

        public long Length => 0;

        public string? PhysicalPath => this.treeEntry.Path;

        public string Name => this.treeEntry.Name;

        public DateTimeOffset LastModified => this.lastModifiedDate;

        public bool IsDirectory => this.treeEntry.Mode == Mode.Directory;

        private GitFileInfo(TreeEntry treeEntry,
                            DateTimeOffset lastModifiedDate)
        {
            this.treeEntry = treeEntry;
            this.lastModifiedDate = lastModifiedDate;
        }

        public Stream CreateReadStream()
        {
            if (IsDirectory)
            {
                return Stream.Null;
            }

            if (this.treeEntry.Target is Blob blob)
            {
                return blob.GetContentStream();
            }

            throw new InvalidOperationException("Target is not a blob.");
        }

        internal static IRadiaFileInfo Create(TreeEntry treeEntry, DateTimeOffset lastModifiedDate)
        {
            if (treeEntry.TargetType is not TreeEntryTargetType.Blob)
                throw new InvalidOperationException("Only blobs can be used for GitFileInfo");

            return new GitFileInfo(treeEntry, lastModifiedDate);
        }
    }
}
