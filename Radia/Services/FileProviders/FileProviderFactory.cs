using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel;

namespace Radia.Services.FileProviders
{
    public class FileProviderFactory : IFileProviderFactory
    {
        public IFileProvider Create(FileProviderConfiguration configuration)
        {
            IFileProvider result = configuration.FileProvider switch
            {
                FileProviderEnum.Git => new GitFileProvider(),
                FileProviderEnum.Local => new LocalFileProvider(),
                _ => throw new InvalidEnumArgumentException(nameof(configuration.FileProvider), (int)configuration.FileProvider, typeof(FileProviderEnum))
            };

            return result;
        }
    }
}
