namespace Radia.Services.ContentProcessors
{
    public interface IContentProcessor
    {
        IContentProcessor SetNext(IContentProcessor contentProcessor);

        IContentResult ProcessContent(string contentType, string content);
    }
}
