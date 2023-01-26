using Microsoft.Extensions.FileProviders;
using System.ComponentModel;
using Radia.Services.FileProviders;
using System.Security.Cryptography;
using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;

namespace Radia.Factories
{
    public class FileProviderFactory : IRadiaFileProviderFactory
    {
        private readonly IEnumerable<IRadiaFileProvider> fileProviders;

        public FileProviderFactory(IEnumerable<IRadiaFileProvider> fileProviders)
        {
            this.fileProviders = fileProviders;
        }

        public IRadiaFileProvider Create(FileProviderConfiguration configuration)
        {
            IRadiaFileProvider? result = configuration.FileProvider switch
            {
                FileProviderEnum.Git => fileProviders.SingleOrDefault((p) => p.FileProviderEnum == FileProviderEnum.Git),
                FileProviderEnum.Local => fileProviders.SingleOrDefault((p) => p.FileProviderEnum == FileProviderEnum.Local),
                _ => throw new InvalidEnumArgumentException(nameof(configuration.FileProvider), (int)configuration.FileProvider, typeof(FileProviderEnum))
            };

            return result ?? throw new InvalidOperationException("No FileProvider found");
        }
    }
}
