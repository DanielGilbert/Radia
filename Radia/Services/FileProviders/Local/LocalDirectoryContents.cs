using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace Radia.Services.FileProviders.Local
{
    public class LocalDirectoryContents : IRadiaDirectoryContents
    {
        private readonly bool allowDirectoryListing;
        private readonly IDirectoryContents directoryContents;
        private IEnumerable<IRadiaFileInfo> emptyList = new List<IRadiaFileInfo>();

        public LocalDirectoryContents(IDirectoryContents directoryContents, bool allowDirectoryListing = true)
        {
            this.allowDirectoryListing = allowDirectoryListing;
            this.directoryContents = directoryContents;
        }

        public bool Exists => this.directoryContents.Exists;

        public IEnumerator<IRadiaFileInfo> GetEnumerator()
        {
            IEnumerable<IRadiaFileInfo> result = this.allowDirectoryListing ? this.directoryContents.Select(p => new LocalFileInfo(p))
                                                                            : this.emptyList;
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
