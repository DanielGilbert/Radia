namespace Radia.Services.FileProviders
{
    public interface IFileProviderConfiguration
    {
        FileProviderEnum FileProvider { get; set; }

        Dictionary<string, string> Settings { get; set; }
    }
}