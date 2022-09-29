namespace Hobbits.Services
{
    public class TimeOfDayProvider
    {
        public DateTime Current { get; } = DateTime.UtcNow;
    }
}
