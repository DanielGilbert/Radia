namespace Radia.Services
{
    public interface IVersionService
    {
        string Version { get; }
        string GetVersionLinked();
    }
}
