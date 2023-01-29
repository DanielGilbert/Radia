using Microsoft.Extensions.FileProviders;
using Radia.Services.FileProviders;

namespace Radia.Factories.FileProvider
{
    public interface IRadiaFileProviderFactory
    {
        IRadiaFileProvider Create(FileProviderConfiguration configuration);
        IRadiaFileProvider CreateComposite(IList<FileProviderConfiguration> configuration);
    }
}
