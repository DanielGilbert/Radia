using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace Radia.Services.FileProviders.Local
{
    public class LocalDirectoryContents : IRadiaDirectoryContents
    {
        private readonly IDirectoryContents directoryContents;

        public LocalDirectoryContents(IDirectoryContents directoryContents)
        {
            this.directoryContents = directoryContents;
        }

        public bool Exists => this.directoryContents.Exists;

        public IEnumerator<IRadiaFileInfo> GetEnumerator()
        {
            IEnumerable<IRadiaFileInfo> result = this.directoryContents.Select(p => new LocalFileInfo(p));
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
