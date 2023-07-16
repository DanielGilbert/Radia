using LibGit2Sharp;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileInfo : IRadiaFileInfo
    {
        private readonly TreeEntry? treeEntry;
        private readonly DateTimeOffset lastModifiedDate;
        private readonly long blobSize;
        private readonly string invalidPath;

        public bool Exists => this.treeEntry != null ? true : false;

        public long Length => blobSize;

        public string? PhysicalPath => this.treeEntry?.Path ?? invalidPath;

        public string Name => this.treeEntry?.Name ?? invalidPath;

        public DateTimeOffset LastModified => this.lastModifiedDate;

        public bool IsDirectory => this.treeEntry?.Mode == Mode.Directory;

        private GitFileInfo(TreeEntry treeEntry,
                            DateTimeOffset lastModifiedDate,
                            long blobSize)
        {
            this.treeEntry = treeEntry;
            this.lastModifiedDate = lastModifiedDate;
            this.blobSize = blobSize;
            this.invalidPath = string.Empty;
        }

        private GitFileInfo(string invalidPath)
        {
            this.treeEntry = null;
            this.lastModifiedDate = DateTimeOffset.MinValue;
            this.invalidPath = invalidPath;
            this.blobSize = 0;
        }

        public Stream CreateReadStream()
        {
            if (IsDirectory)
            {
                return Stream.Null;
            }

            if (this.treeEntry?.Target is Blob blob)
            {
                return blob.GetContentStream();
            }

            throw new InvalidOperationException("Target is not a blob.");
        }

        internal static IRadiaFileInfo CreateInvalid(string invalidPath)
        {
            return new GitFileInfo(invalidPath);
        }

        internal static IRadiaFileInfo Create(Repository repository, TreeEntry treeEntry, DateTimeOffset lastModifiedDate)
        {
            if (treeEntry.TargetType is not TreeEntryTargetType.Blob || treeEntry.Target is not Blob blob)
                throw new InvalidOperationException("Only blobs can be used for GitFileInfo");

            var blobSize = repository.ObjectDatabase.RetrieveObjectMetadata(blob.Id).Size;

            return new GitFileInfo(treeEntry, lastModifiedDate, blobSize);
        }

        internal static IRadiaFileInfo Create(Repository repository, string subpath)
        {
            var currentCommit = repository.Head.Tip;

            var treeEntry = currentCommit[subpath];

            if (treeEntry?.Mode == Mode.Directory)
            {
                return CreateInvalid(subpath);
            }
            else if (treeEntry?.TargetType == TreeEntryTargetType.Blob)
            {
                return Create(repository, treeEntry, currentCommit.Author.When);
            }

            return CreateInvalid(subpath);
        }
    }
}
