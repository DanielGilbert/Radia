using Microsoft.Extensions.FileProviders;

namespace Dagidirli.Services.FileProviders
{
    public interface IFileProviderFactory
    {
        IFileProvider Create(FileProviderConfiguration configuration);
    }
}
