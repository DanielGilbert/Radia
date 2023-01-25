using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileProvider : IFileProvider
    {
        private readonly IFileProvider fileProvider;
        private readonly string rootPath;

        public LocalFileProvider(IConfigurationService configurationService)
        {
            var fileProviderConfiguration = configurationService.GetFileProviderConfiguration();
            if (fileProviderConfiguration.FileProvider == FileProviderEnum.Local)
            {
                this.rootPath = fileProviderConfiguration.Settings["RootDirectory"];
                this.fileProvider = new PhysicalFileProvider(this.rootPath);
            }
            else
            {
                this.rootPath = string.Empty;
                this.fileProvider = new EmptyFileProvider();
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

            /*
            if (!result.StartsWith(this.rootPath))
            {
                if (result.StartsWith('~'))
                {
                    result = result.Replace("~", this.rootPath);
                }
                else
                {
                    result = this.rootPath + result;
                }
            }
            */
            return result;
        }

    }
}
