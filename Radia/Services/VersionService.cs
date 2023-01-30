using System.Reflection;

namespace Radia.Services
{
    public class VersionService : IVersionService
    {
        public string Version => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        public string GetVersionLinked()
        {
            string result = $"<a href=\"https://github.com/DanielGilbert/Radia/releases/tag/v{Version}\">v{Version}</a>";

            return result;
        }
    }
}
