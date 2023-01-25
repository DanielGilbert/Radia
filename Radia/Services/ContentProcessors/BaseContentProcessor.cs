namespace Radia.Services.ContentProcessors
{
    public abstract class BaseContentProcessor : IContentProcessor<string>
    {
        private IContentProcessor<string> nextContentProcessor;

        public IContentProcessor<string> SetNext(IContentProcessor<string> contentProcessor)
        {
            this.nextContentProcessor = contentProcessor;
            return contentProcessor;
        }

        public virtual IContentResult<string> ProcessContent(string contentType, Stream content)
        {
            if (this.nextContentProcessor != null)
            {
                return this.nextContentProcessor.ProcessContent(contentType, content);
            }
            else
            {
                return new EmptyContentResult();
            }
        }
    }
}
