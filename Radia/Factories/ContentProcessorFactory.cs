using Radia.Services.ContentProcessors;

namespace Radia.Factories
{
    public class ContentProcessorFactory : IContentProcessorFactory<string>
    {
        public IContentProcessor<string> Create()
        {
            var result = new MarkdownContentProcessor().SetNext(new HtmlContentProcessor());

            return result;
        }
    }
}
