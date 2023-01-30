using System.Diagnostics.CodeAnalysis;

namespace Radia.Services
{
    /// <remarks>
    /// For now, this is explicitly excluded from CodeCoverage,
    /// until I come up with a smart way to test DateTime.Now...
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
