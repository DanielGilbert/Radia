using System.Reflection;

namespace Radia.Services
{
    public class VersionService : IVersionService
    {
        public string Version { get; }

        public VersionService(string version)
        {
            Version = version;
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
