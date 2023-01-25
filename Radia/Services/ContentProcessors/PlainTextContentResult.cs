namespace Radia.Services.ContentProcessors
{
    public class PlainTextContentResult : IContentResult<string>
    {
        public string Result { get; }
        public Stream Stream { get; }

        public PlainTextContentResult(string result, Stream stream)
        {
            Result = result;
            Stream = stream;
        }
    }
}
