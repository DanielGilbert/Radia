using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;

namespace Radia.Factories.FileProvider
{
    public class FileProviderFactory : IRadiaFileProviderFactory
    {
        public IRadiaFileProvider Create(FileProviderConfiguration configuration)
        {
            IRadiaFileProvider? result = null;

            if (configuration.Settings.TryGetValue("RootDirectory", out string? value))
            {
                result = new LocalFileProvider(value, configuration.AllowListing);
            }

            return result ?? throw new InvalidOperationException("No FileProvider found");
        }

        public IRadiaFileProvider CreateComposite(IList<FileProviderConfiguration> configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            IList<IRadiaFileProvider> fileProviders = new List<IRadiaFileProvider>();

            foreach (var config in configuration)
            {
                fileProviders.Add(Create(config));
            }

            IRadiaFileProvider compositeRadiaFileProvider = new CompositeRadiaFileProvider(fileProviders);
            return compositeRadiaFileProvider;
        }
    }
}
