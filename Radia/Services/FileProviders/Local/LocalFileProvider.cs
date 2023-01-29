using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Radia.Exceptions;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileProvider : IRadiaFileProvider
    {
        private readonly bool allowDirectoryListing;
        private readonly IFileProvider fileProvider;
        private readonly string rootPath;

        public LocalFileProvider(string rootPath, bool allowDirectoryListing)
        {
            this.rootPath = rootPath;
            this.allowDirectoryListing = allowDirectoryListing;
            this.fileProvider = new PhysicalFileProvider(this.rootPath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            if (this.allowDirectoryListing is false)
            {
                return new EmptyDirectoryContents();
            }
            return this.fileProvider.GetDirectoryContents(subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return this.fileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return this.fileProvider.Watch(filter);
        }
    }
}
