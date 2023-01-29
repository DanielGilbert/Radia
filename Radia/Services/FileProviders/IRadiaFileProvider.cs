using Microsoft.Extensions.FileProviders;

namespace Radia.Services.FileProviders
{
    public interface IRadiaFileProvider : IFileProvider
    {
        FileProviderEnum FileProviderEnum { get; }
        char PathDelimiter { get; }
    }
}
