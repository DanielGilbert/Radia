using Radia.Services.FileProviders;

namespace Radia.Models
{
    public class AppConfiguration
    {
        public FileProviderConfiguration? FileProviderConfiguration { get; set; }
        public string? WebsiteTitle { get; set; }
        public string? DefaultPageHeader { get; set; }
    }
}
