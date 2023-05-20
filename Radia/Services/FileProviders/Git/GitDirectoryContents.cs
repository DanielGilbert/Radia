using System.Collections;

namespace Radia.Services.FileProviders.Git
{
    public class GitDirectoryContents : IRadiaDirectoryContents
    {
        private readonly IRadiaDirectoryContents radiaDirectoryContents;

        public bool Exists => this.radiaDirectoryContents.Exists;

        public GitDirectoryContents()
        {
            this.radiaDirectoryContents = null;
        }

        public IEnumerator<IRadiaFileInfo> GetEnumerator()
        {
            return this.radiaDirectoryContents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.radiaDirectoryContents.GetEnumerator();
        }
    }
}
