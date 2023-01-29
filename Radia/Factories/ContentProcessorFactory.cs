using Radia.Services.ContentProcessors;

namespace Radia.Factories
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
