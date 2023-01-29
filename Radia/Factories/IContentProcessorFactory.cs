using Radia.Services.ContentProcessors;

namespace Radia.Factories
{
    public interface IContentProcessorFactory
    {
        IContentProcessor Create();
    }
}
