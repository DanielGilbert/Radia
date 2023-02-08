namespace Radia.Services.FileProviders
{
    /// <summary>
    /// This interface allows for network related functionality,
    /// such as fetching new data from the remote source
    /// beforehand
    /// </summary>
    public interface IRadiaNetworkFileProvider
    {
        /// <summary>
        /// Should fetch all changes from the remote source,
        /// so that they can be reflected on the local source.
        /// </summary>
        void Fetch();
    }
}
