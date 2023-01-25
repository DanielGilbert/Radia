namespace Radia.Services.ContentProcessors
{
    public class PlainTextContentResult : IContentResult<string>
    {
        public string Result { get; }
        public Stream Stream { get; }

        public PlainTextContentResult(string result, Stream stream, string contentType)
        {
            if (contentType.StartsWith("text/"))
            {
                using(var reader = new StreamReader(stream))
                {
                    Result = reader.ReadToEnd();
                }
            }
            else
            {
                Result = result;
            }
            Stream = stream;
        }
    }
}
