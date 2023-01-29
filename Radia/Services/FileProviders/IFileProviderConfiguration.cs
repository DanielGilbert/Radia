namespace Radia.Services.FileProviders
{
    public interface IFileProviderConfiguration
    {
        bool AllowListing { get; }
        Dictionary<string, string> Settings { get; set; }
    }
}