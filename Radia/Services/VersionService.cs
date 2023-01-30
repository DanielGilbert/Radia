using System.Reflection;

namespace Radia.Services
{
    public class VersionService : IVersionService
    {
        public string Version { get; }

        public VersionService()
        {
            Version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? String.Empty; 
        }

        public string GetVersionLinked()
        {
            if (string.IsNullOrWhiteSpace(Version))
            {
                return string.Empty;
            }

            string result = $"<a href=\"https://github.com/DanielGilbert/Radia/releases/tag/v{Version}\">v{Version}</a>";

            return result;
        }
    }
}
