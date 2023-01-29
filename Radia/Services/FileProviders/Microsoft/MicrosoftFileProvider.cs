using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Microsoft
{
    public class MicrosoftFileProvider : IFileProvider
    {
        private readonly IRadiaFileProvider radiaFileProvider;

        public MicrosoftFileProvider(IRadiaFileProvider radiaFileProvider)
        {
            this.radiaFileProvider = radiaFileProvider;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new MicrosoftDirectoryContents(this.radiaFileProvider.GetDirectoryContents(subpath));
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new MicrosoftFileInfo(this.radiaFileProvider.GetFileInfo(subpath));
        }

        public IChangeToken Watch(string filter)
        {
            return this.radiaFileProvider.Watch(filter);
        }
    }
}
