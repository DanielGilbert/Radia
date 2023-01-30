namespace Radia.Services
{
    public interface IDateTimeService
    {
        DateTime Now();
        DateTime UtcNow();
    }
}
