using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileProvider : IRadiaFileProvider
    {
        private readonly IFileProvider fileProvider;
        private readonly string rootPath;

        public FileProviderEnum FileProviderEnum { get; }

        public LocalFileProvider(IConfigurationService configurationService, IFileProvider? frameworkFileProvider = null)
        {
            var fileProviderConfiguration = configurationService.GetFileProviderConfiguration();
            if (fileProviderConfiguration.FileProvider == FileProviderEnum.Local)
            {
                this.rootPath = fileProviderConfiguration.Settings["RootDirectory"];
                if (frameworkFileProvider == null)
                {
                    this.fileProvider = new PhysicalFileProvider(this.rootPath);
                }
                else
                {
                    this.fileProvider = frameworkFileProvider;
                }

                this.FileProviderEnum = FileProviderEnum.Local;
            }
            else
            {
                this.rootPath = string.Empty;
                this.fileProvider = new EmptyFileProvider();
                this.FileProviderEnum = FileProviderEnum.Empty;
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            subpath = EnsureFullPath(subpath);
            return this.fileProvider.GetDirectoryContents(subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            subpath = EnsureFullPath(subpath);
            return this.fileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return this.fileProvider.Watch(filter);
        }
        private string EnsureFullPath(string subpath)
        {
            var result = subpath;

            if (result.StartsWith('~'))
            {
                result = result.Replace("~", string.Empty);
            }

            return result;
        }

    }
}
