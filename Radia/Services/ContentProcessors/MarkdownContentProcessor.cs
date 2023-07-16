using Markdig;

namespace Radia.Services.ContentProcessors
{
    public class MarkdownContentProcessor : BaseContentProcessor
    {
        public override string ProcessContent(string contentType, string content)
        {
            if (contentType.ToLowerInvariant().Contains("text/markdown"))
            {
                var pipeline = new MarkdownPipelineBuilder().DisableHtml().UseAdvancedExtensions().Build();

                var result = Markdown.ToHtml(content, pipeline).Trim();
                    
                return base.ProcessContent(contentType, result);
                
            }
            return base.ProcessContent(contentType, content);
        }
    }
}
