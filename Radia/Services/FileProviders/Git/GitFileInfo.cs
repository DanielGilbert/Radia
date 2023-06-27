using LibGit2Sharp;
using System.Runtime.CompilerServices;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileInfo : IRadiaFileInfo
    {
        private readonly TreeEntry? treeEntry;
        private readonly DateTimeOffset lastModifiedDate;
        private readonly string invalidPath;

        public bool Exists => this.treeEntry != null ? true : false;

        public long Length => 0;

        public string? PhysicalPath => this.treeEntry?.Path ?? invalidPath;

        public string Name => this.treeEntry?.Name ?? invalidPath;

        public DateTimeOffset LastModified => this.lastModifiedDate;

        public bool IsDirectory => this.treeEntry?.Mode == Mode.Directory;

        private GitFileInfo(TreeEntry treeEntry,
                            DateTimeOffset lastModifiedDate)
        {
            this.treeEntry = treeEntry;
            this.lastModifiedDate = lastModifiedDate;
        }

        private GitFileInfo(string invalidPath)
        {
            this.treeEntry = null;
            this.lastModifiedDate = DateTimeOffset.MinValue;
            this.invalidPath = invalidPath;
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

        internal static IRadiaFileInfo CreateInvalid(string invalidPath)
        {
            return new GitFileInfo(invalidPath);
        }

        internal static IRadiaFileInfo Create(TreeEntry treeEntry, DateTimeOffset lastModifiedDate)
        {
            if (treeEntry.TargetType is not TreeEntryTargetType.Blob)
                throw new InvalidOperationException("Only blobs can be used for GitFileInfo");

            return new GitFileInfo(treeEntry, lastModifiedDate);
        }

        internal static IRadiaFileInfo Create(Repository repository, string subpath)
        {
            var currentCommit = repository.Head.Tip;

            var treeEntry = currentCommit[subpath];

            if (treeEntry?.TargetType == TreeEntryTargetType.Tree)
            {
                if (treeEntry.Target is Tree subtree)
                {
                    foreach (var entry in subtree)
                    {
                        if (entry.Mode == Mode.Directory)
                        {
                            return CreateInvalid(entry.Path);
                        }
                        else
                        {
                            return Create(entry, currentCommit.Author.When);
                        }
                    }
                }
            }
            else if (treeEntry?.TargetType == TreeEntryTargetType.Blob)
            {
                return Create(treeEntry, currentCommit.Author.When);
            }

            return CreateInvalid(subpath);
        }

        private static bool IsRoot(string subpath)
        {
            return String.IsNullOrEmpty(subpath) || subpath.Equals("\\") || subpath.Equals("/");
        }

        //private static TreeEntry GetTreeEntryForPath(Commit currentCommit, string subpath)
        //{
        //    return IsRoot(subpath) ? currentCommit.Tree : currentCommit[subpath];
        //}

        //private static bool IsRoot(string subpath)
        //{
        //    return string.IsNullOrEmpty(subpath) || subpath.Equals("\\") || subpath.Equals("/"); 
        //}
    }
}
