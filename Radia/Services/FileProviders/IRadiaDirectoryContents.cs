using Microsoft.Extensions.FileProviders;
using Radia.Models;

namespace Radia.Services.FileProviders
{
    public interface IRadiaDirectoryContents : IEnumerable<IRadiaFileInfo>
    {
        /// <summary>
        /// True if a directory was located at the given path.
        /// </summary>
        bool Exists { get; }
    }
}
