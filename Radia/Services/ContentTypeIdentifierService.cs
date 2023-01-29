using Microsoft.AspNetCore.StaticFiles;

namespace Radia.Services
{
    public class ContentTypeIdentifierService : IContentTypeIdentifierService
    {
        private readonly FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public ContentTypeIdentifierService()
        {
            this.fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
        }

        public string GetContentTypeFrom(string filePath)
        {
            if (fileExtensionContentTypeProvider.TryGetContentType(filePath, out string? contentType))
            {
                return contentType;
            }

            return string.Empty;
        }
    }
}
