namespace Radia.Services.ContentProcessors
{
    public interface IContentProcessor
    {
        IContentProcessor SetNext(IContentProcessor contentProcessor);

        string ProcessContent(string contentType, string content);
    }
}
