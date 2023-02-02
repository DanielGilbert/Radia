using System.Collections;

namespace Radia.Services.FileProviders.Git
{
    public class GitDirectoryContents : IRadiaDirectoryContents
    {
        public bool Exists => throw new NotImplementedException();

        public IEnumerator<IRadiaFileInfo> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
