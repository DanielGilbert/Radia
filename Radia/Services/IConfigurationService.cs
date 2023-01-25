using Radia.Services.FileProviders;

namespace Radia.Services
{
    public interface IConfigurationService
    {
        FileProviderConfiguration GetFileProviderConfiguration();
        string GetWebsiteTitle();
        string GetPageHeader();
    }
}
