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
            Url = new Uri(webHost + '/' + relativePath.Replace(pathSeparatorChar, '/').TrimStart('/').TrimEnd('/')).ToString();
        }
    }
}
