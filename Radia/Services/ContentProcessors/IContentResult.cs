namespace Radia.Services.ContentProcessors
{
    public interface IContentResult<T>
    {
        Stream Stream { get; }
        T Result { get; }
    }
}
