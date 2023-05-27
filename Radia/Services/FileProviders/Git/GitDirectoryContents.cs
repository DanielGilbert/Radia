using LibGit2Sharp;
using System.Collections;

namespace Radia.Services.FileProviders.Git
{
    public class GitDirectoryContents : IRadiaDirectoryContents
    {
        private readonly Repository repository;
        private readonly string subpath;
        private readonly string branch;
        private readonly string headBranch;
        private bool isRoot => IsRoot(this.subpath);

        public bool Exists => true;

        public GitDirectoryContents(Repository repository, string branch, string subpath)
        {
            this.repository = repository;
            this.subpath = subpath;
            this.branch = branch;
            this.headBranch = $"refs/heads/{branch}";
        }

        public IEnumerator<IRadiaFileInfo> GetEnumerator()
        {
            var currentCommit = this.repository.Head.Tip;

            if (isRoot)
            {
                foreach(var entry in currentCommit.Tree)
                {
                    if (entry.Mode == Mode.Directory)
                    {
                        yield return GitDirectoryInfo.Create(entry, currentCommit.Author.When);
                    }
                    else
                    {
                        yield return GitFileInfo.Create(entry, currentCommit.Author.When);
                    }
                }
            }
            else
            {
                var treeEntry = currentCommit[this.subpath];

                yield return null;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool IsRoot(string subpath)
        {
            return subpath == string.Empty
                   || subpath == "/"
                   || subpath == "\\";
        }

    }
}
