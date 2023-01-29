namespace Radia.ViewModels
{
    public interface IProcessedFileViewModel : IPhysicalFileViewModel
    {
        string Content { get; }
    }
}
