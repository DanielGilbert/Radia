using Microsoft.Extensions.FileProviders;

namespace Radia.Models
{
    public interface IRadiaFileInfo : IFileInfo
    {
        IRadiaFileInfo Ancestor { get; }
        bool IsRoot { get; }
    }
}
