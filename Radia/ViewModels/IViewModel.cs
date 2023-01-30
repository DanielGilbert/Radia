namespace Radia.ViewModels
{
    public interface IViewModel
    {
        string PageTitle { get; }
        string PageHeader { get; }
        string WebsiteRoot { get; }
        string FooterContent { get; }
    }
}
