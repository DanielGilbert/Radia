namespace Radia.Services.FileProviders
{
    public class FileProviderConfiguration : IFileProviderConfiguration
    {
        public Dictionary<string, string> Settings { get; set; }

        public bool AllowListing { get; set; }

        public FileProviderConfiguration()
        {
            AllowListing = true;
            Settings = new Dictionary<string, string>();
        }
    }
}
