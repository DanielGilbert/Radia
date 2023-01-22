using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Dagidirli.Services.FileProviders.Git
{
    public class GitFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
