using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Git
{
    public class GitChangeToken : IChangeToken
    {
        public bool ActiveChangeCallbacks => throw new NotImplementedException();

        public bool HasChanged => throw new NotImplementedException();

        public IDisposable RegisterChangeCallback(Action<object?> callback, object? state)
        {
            throw new NotImplementedException();
        }
    }
}
