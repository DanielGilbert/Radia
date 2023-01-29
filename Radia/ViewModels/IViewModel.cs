namespace Radia.ViewModels
{
    public interface IViewModel
    {
        string PageTitle { get; }
        string PageHeader { get; }
        string Content { get; }
        string RelativePath { get; }
        string WebsiteRoot { get; }
    }
}
