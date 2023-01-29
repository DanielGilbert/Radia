using System.IO;

namespace Radia.Models
{
    public class RadiaAncestorInfo : IRadiaAncestorInfo
    {
        public string Url { get; }
        public string Name { get; }
        public string RelativePath { get; }

        public RadiaAncestorInfo(string webHost, string relativePath, string name, char pathSeparatorChar)
        {
            RelativePath = relativePath;
            Name = name;
            Url = CreateUrl(webHost, relativePath, name, true);
        }

        private string CreateUrl(string webHost, string path, string name, bool isDirectory)
        {
            IList<string> components = path.Split('/').ToList();
            components.Add(name);
            components = components.Where(x => string.IsNullOrWhiteSpace(x) is false).ToList();
            string completePath = string.Join('/', components.ToArray());
            return new Uri(webHost + '/' + completePath).ToString();
        }
    }
}
