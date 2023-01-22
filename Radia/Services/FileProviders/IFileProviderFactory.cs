using Microsoft.Extensions.FileProviders;

namespace Radia.Services.FileProviders
{
    public interface IFileProviderFactory
    {
        IFileProvider Create(FileProviderConfiguration configuration);
    }
}
