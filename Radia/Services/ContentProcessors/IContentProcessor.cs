namespace Radia.Services.ContentProcessors
{
    public interface IContentProcessor<T>
    {
        IContentProcessor<T> SetNext(IContentProcessor<T> contentProcessor);

        IContentResult<T> ProcessContent(string contentType, Stream content);
    }
}
