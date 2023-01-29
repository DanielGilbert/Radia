namespace Radia.Factories
{
    public class ViewModelFactoryArgs
    {
        public string Path { get; }
        public string PageTitle { get; }
        public ViewModelFactoryArgs(string path, string pageTitle)
        {
            Path = path;
            PageTitle = pageTitle;
        }
    }
}
