namespace Radia.Services.ContentProcessors
{
    public abstract class BaseContentProcessor : IContentProcessor
    {
        private IContentProcessor? nextContentProcessor;

        public IContentProcessor SetNext(IContentProcessor contentProcessor)
        {
            this.nextContentProcessor = contentProcessor;
            return contentProcessor;
        }

        public virtual IContentResult ProcessContent(string contentType, string content)
        {
            if (this.nextContentProcessor != null)
            {
                return this.nextContentProcessor.ProcessContent(contentType, content);
            }
            else
            {
                return new PlainTextContentResult(content, contentType);
            }
        }
    }
}
