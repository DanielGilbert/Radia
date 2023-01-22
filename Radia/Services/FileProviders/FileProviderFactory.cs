using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel;

namespace Radia.Services.FileProviders
{
    public class FileProviderFactory : IFileProviderFactory
    {
        private readonly IFileProvider gitProvider;
        private readonly IFileProvider localProvider;

        public FileProviderFactory(IFileProvider gitProvider,
                                   IFileProvider localProvider)
        {
            this.gitProvider = gitProvider;
            this.localProvider = localProvider;
        }

        public IFileProvider Create(FileProviderConfiguration configuration)
        {
            IFileProvider result = configuration.FileProvider switch
            {
                FileProviderEnum.Git => gitProvider,
                FileProviderEnum.Local => localProvider,
                _ => throw new InvalidEnumArgumentException(nameof(configuration.FileProvider), (int)configuration.FileProvider, typeof(FileProviderEnum))
            };

            return result;
        }
    }
}
