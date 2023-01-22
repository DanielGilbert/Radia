using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace Dagidirli.Services.FileProviders.Local
{
    public class LocalDirectoryContents : IDirectoryContents
    {
        public bool Exists => throw new NotImplementedException();

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
