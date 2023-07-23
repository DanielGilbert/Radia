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
                    var result = this.repository.Commits.QueryBy(entry.Name);

                    if (entry.Mode == Mode.Directory)
                    {
                        yield return GitDirectoryInfo.Create(entry, result.Last().Commit.Author.When);
                    }
                    else
                    {
                        yield return GitFileInfo.Create(this.repository, entry, result.Last().Commit.Author.When);
                    }
                }
            }
            else
            {
                var treeEntry = currentCommit[this.subpath];

                if (treeEntry.TargetType == TreeEntryTargetType.Tree)
                {
                    if (treeEntry.Target is Tree subtree)
                    {
                        foreach (var entry in subtree)
                        {
                            if (entry.Name.StartsWith("."))
                                continue;

                            var result = this.repository.Commits.QueryBy(this.subpath + entry.Name);

                            if (entry.Mode == Mode.Directory)
                            {

                                yield return GitDirectoryInfo.Create(entry, result.Last().Commit.Author.When);
                            }
                            else
                            {
                                yield return GitFileInfo.Create(this.repository, entry, result.Last().Commit.Author.When);
                            }
                        }
                    }
                }
                else if (treeEntry.TargetType == TreeEntryTargetType.Blob)
                {
                    yield return GitFileInfo.Create(this.repository, treeEntry, currentCommit.Author.When);
                }
            }

            yield break;
        }

        /// <summary>
        /// For some reasons, the libgit2sharp implementation doubles the first part of the path.
        /// I'm not sure why, but this tries to mitigate this issue, without breaking everything.
        /// </summary>
        /// <param name="path">The path to clean</param>
        /// <returns>The cleaned path.</returns>
        private string FixEntryPath(string path)
        {
            return path;
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
