using Microsoft.Extensions.FileProviders;
using Radia.Services.FileProviders;

namespace Radia.Factories
{
    public interface IRadiaFileProviderFactory
    {
        IRadiaFileProvider Create(FileProviderConfiguration configuration);
    }
}
