using Microsoft.Extensions.FileProviders;
using System.Collections;

namespace Radia.Services.FileProviders.Git
{
    public class GitDirectoryContents : IDirectoryContents
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
