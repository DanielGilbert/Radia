using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

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

        public IRadiaDirectoryContents GetDirectoryContents(string subpath)
        {
            if (this.allowDirectoryListing is false)
            {
                return new EmptyDirectoryContents();
            }
            return new LocalDirectoryContents(this.fileProvider.GetDirectoryContents(subpath));
        }

        public IRadiaFileInfo GetFileInfo(string subpath)
        {
            return new LocalFileInfo(this.fileProvider.GetFileInfo(subpath));
        }

        public IChangeToken Watch(string filter)
        {
            return this.fileProvider.Watch(filter);
        }
    }
}
