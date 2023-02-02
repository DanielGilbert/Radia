using Radia.Services.ContentProcessors;

namespace Radia.Factories.ContentProcessor
{
    public interface IContentProcessorFactory
    {
        IContentProcessor Create();
    }
}
