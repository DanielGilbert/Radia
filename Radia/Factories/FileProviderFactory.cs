using Microsoft.Extensions.FileProviders;
using System.ComponentModel;
using Radia.Services.FileProviders;
using System.Security.Cryptography;
using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;

namespace Radia.Factories
{
    public class FileProviderFactory : IFileProviderFactory
    {
        private readonly IEnumerable<IFileProvider> fileProviders;

        public FileProviderFactory(IEnumerable<IFileProvider> fileProviders)
        {
            this.fileProviders = fileProviders;
        }

        public IFileProvider Create(FileProviderConfiguration configuration)
        {
            IFileProvider? result = configuration.FileProvider switch
            {
                FileProviderEnum.Git => fileProviders.SingleOrDefault((p) => p.GetType() == typeof(GitFileProvider)),
                FileProviderEnum.Local => fileProviders.SingleOrDefault((p) => p.GetType() == typeof(LocalFileProvider)),
                _ => throw new InvalidEnumArgumentException(nameof(configuration.FileProvider), (int)configuration.FileProvider, typeof(FileProviderEnum))
            };

            return result ?? throw new InvalidOperationException("No FileProvider found");
        }
    }
}
