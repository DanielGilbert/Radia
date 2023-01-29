using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace Radia.Services.FileProviders
{
    public class EmptyDirectoryContents : IRadiaDirectoryContents
    {
        public bool Exists => true;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new List<IRadiaFileInfo>().GetEnumerator();
        }

        IEnumerator<IRadiaFileInfo> IEnumerable<IRadiaFileInfo>.GetEnumerator()
        {
            return new List<IRadiaFileInfo>().GetEnumerator();
        }
    }
}
