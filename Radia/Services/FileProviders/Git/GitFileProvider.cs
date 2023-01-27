using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace Radia.Services.FileProviders.Git
{
    /// <summary>
    /// As long as this class is not implemented, it should not count towards code coverage
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GitFileProvider : IRadiaFileProvider
    {
        public FileProviderEnum FileProviderEnum => FileProviderEnum.Git;

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
