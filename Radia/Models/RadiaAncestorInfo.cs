using System.IO;

namespace Radia.Models
{
    public class RadiaAncestorInfo : IRadiaAncestorInfo
    {
        public string Url { get; }
        public string Name { get; }
        public string RelativePath { get; }

        public RadiaAncestorInfo(string url, string relativePath, string name)
        {
            Url = url;
            RelativePath = relativePath;
            Name = name;
        }
    }
}
