using Radia.Services.ContentProcessors;

namespace Radia.Factories
{
    public interface IContentProcessorFactory<T>
    {
        IContentProcessor<T> Create();
    }
}
