using Markdig;
using System.IO;

namespace Radia.Services.ContentProcessors
{
    public class MarkdownContentProcessor : BaseContentProcessor
    {
        public override IContentResult ProcessContent(string contentType, string content)
        {
            if (contentType.ToLowerInvariant().Contains("text/markdown"))
            {
                var pipeline = new MarkdownPipelineBuilder().DisableHtml().Build();

                var result = Markdig.Markdown.ToHtml(content, pipeline);
                    
                return base.ProcessContent(contentType, result);
                
            }
            return base.ProcessContent(contentType, content);
        }
    }
}
