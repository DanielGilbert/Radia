using Radia.Services.FileProviders;

namespace Radia.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService(IConfiguration configuration)
        {

        }

        public FileProviderConfiguration GetFileProviderConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
