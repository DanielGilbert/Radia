using Microsoft.Extensions.FileProviders;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;

namespace Radia.ViewModels
{
    public interface IPhysicalFileViewModel : IViewModel
    {
        IRadiaFileInfo FileInfo { get; }
        string? ContentType { get; }
    }
}
