using Microsoft.Extensions.FileProviders;
using Radia.Services.ContentProcessors;
using Radia.Services.FileProviders;

namespace Radia.ViewModels
{
    public interface IPhysicalFileViewModel : IViewModel
    {
        IRadiaFileInfo FileInfo { get; }
        IContentResult ContentResult { get; }
        string? ContentType { get; }
        string? FileName { get; }
    }
}
