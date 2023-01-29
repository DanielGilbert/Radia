using Microsoft.Extensions.FileProviders;
using Radia.Services.FileProviders.Local;
using System.Collections;

namespace Radia.Services.FileProviders.Microsoft
{
    public class MicrosoftDirectoryContents : IDirectoryContents
    {
        private IRadiaDirectoryContents radiaDirectoryContents;

        public MicrosoftDirectoryContents(IRadiaDirectoryContents radiaDirectoryContents)
        {
            this.radiaDirectoryContents = radiaDirectoryContents;
        }

        public bool Exists => radiaDirectoryContents.Exists;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            IEnumerable<IFileInfo> result = this.radiaDirectoryContents.Select(p => new MicrosoftFileInfo(p));
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
