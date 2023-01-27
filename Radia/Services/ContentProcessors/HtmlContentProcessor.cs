namespace Radia.Services.ContentProcessors
{
    public class HtmlContentProcessor : BaseContentProcessor
    {
        public override IContentResult<string> ProcessContent(string contentType, Stream content)
        {
            return base.ProcessContent(contentType, content);
        }
    }
}
