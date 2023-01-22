namespace Radia.Services.FileProviders
{
    public class FileProviderConfiguration
    {
        public FileProviderEnum FileProvider { get; set; }

        public Dictionary<string, string> Settings { get; set; }

        public FileProviderConfiguration()
        {
            Settings = new Dictionary<string, string>();
        }
    }

    public enum FileProviderEnum
    {
        Local = 0,
        Git = 1
    }
}
