using Microsoft.Extensions.FileProviders;
using Radia.Services.ContentProcessors;

namespace Radia.ViewModels
{
    public interface IPhysicalFileViewModel : IViewModel
    {
        IFileInfo FileInfo { get; }
        IContentResult ContentResult { get; }
        string? ContentType { get; }
        string? FileName { get; }
    }
}
