namespace Radia.Services.ContentProcessors
{
    public class MarkdownContentProcessor : BaseContentProcessor
    {
        public override IContentResult<string> ProcessContent(string contentType, Stream content)
        {
            return base.ProcessContent(contentType, content);
        }
    }
}
