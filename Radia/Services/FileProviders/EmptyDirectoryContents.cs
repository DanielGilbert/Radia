using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace Radia.Services.FileProviders
{
    public class EmptyDirectoryContents : IRadiaDirectoryContents
    {
        public bool Exists => true;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            return new List<IFileInfo>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new List<IFileInfo>().GetEnumerator();
        }
    }
}
