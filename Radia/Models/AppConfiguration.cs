using Radia.Services.FileProviders;

namespace Radia.Models
{
    public class AppConfiguration
    {
        public IDictionary<string, FileProviderConfiguration>? FileProviderConfigurations { get; set; }
        public string? WebsiteTitle { get; set; }
        public string? DefaultPageHeader { get; set; }
    }
}
