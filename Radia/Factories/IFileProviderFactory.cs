using Microsoft.Extensions.FileProviders;
using Radia.Services.FileProviders;

namespace Radia.Factories
{
    public interface IFileProviderFactory
    {
        IFileProvider Create(FileProviderConfiguration configuration);
    }
}
