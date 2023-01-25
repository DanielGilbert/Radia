using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileProvider : IFileProvider
    {
        private readonly IFileProvider fileProvider;

        public LocalFileProvider(IConfigurationService configurationService)
        {
            var fileProviderConfiguration = configurationService.GetFileProviderConfiguration();
            if (fileProviderConfiguration.FileProvider == FileProviderEnum.Local)
            {
                this.fileProvider = new PhysicalFileProvider(fileProviderConfiguration.Settings["RootDirectory"]);
            }
            else
            {
                this.fileProvider = new EmptyFileProvider();
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
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
