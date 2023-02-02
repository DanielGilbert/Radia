using Radia.Services.ContentProcessors;

namespace Radia.Factories.ContentProcessor
{
    public class ContentProcessorFactory : IContentProcessorFactory
    {
        public IContentProcessor Create()
        {
            var result = new MarkdownContentProcessor();

            return result;
        }
    }
}
