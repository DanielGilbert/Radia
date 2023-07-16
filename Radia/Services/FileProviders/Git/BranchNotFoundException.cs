using System.Runtime.Serialization;

namespace Radia.Services.FileProviders.Git
{
    [Serializable]
    internal class BranchNotFoundException : Exception
    {
        public BranchNotFoundException()
        {
        }

        public BranchNotFoundException(string? message) : base(message)
        {
        }

        public BranchNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BranchNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}