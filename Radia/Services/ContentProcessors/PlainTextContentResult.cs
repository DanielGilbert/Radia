namespace Radia.Services.ContentProcessors
{
    public class PlainTextContentResult : IContentResult
    {
        public string Result { get; }

        public PlainTextContentResult(string result, string contentType)
        {
            Result = result;
        }
    }
}
