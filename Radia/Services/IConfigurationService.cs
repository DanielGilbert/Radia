using Radia.Services.FileProviders;

namespace Radia.Services
{
    public interface IConfigurationService
    {
        IList<FileProviderConfiguration> GetFileProviderConfigurations();
        string GetWebsiteTitle();
        string GetPageHeader();
    }
}
