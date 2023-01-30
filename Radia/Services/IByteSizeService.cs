namespace Radia.Services
{
    public interface IByteSizeService
    {
        string Build(ulong size);
        string Build(ulong size, ByteSizeOptions byteSizeOptions);
    }
}
