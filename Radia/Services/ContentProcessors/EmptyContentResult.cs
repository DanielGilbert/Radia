namespace Radia.Services.ContentProcessors
{
    internal class EmptyContentResult : IContentResult<string>
    {
        public Stream Stream => Stream.Null;

        public string Result => string.Empty;
    }
}