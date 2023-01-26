using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders
{
    public class EmptyFileProvider : IRadiaFileProvider
    {
        public FileProviderEnum FileProviderEnum => FileProviderEnum.Empty;

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
