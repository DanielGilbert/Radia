using Microsoft.Extensions.FileProviders;

namespace Radia.Models
{
    public interface IRadiaFileInfo : IFileInfo
    {
        bool IsRoot { get; }
        string Url { get; }
    }
}
