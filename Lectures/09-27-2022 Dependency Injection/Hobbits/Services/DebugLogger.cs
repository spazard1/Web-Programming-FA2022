using System.Diagnostics;

namespace Hobbits.Services
{
    public class DebugLogger : IHobbitLogger
    {
        private readonly IRequestIdGenerator requestIdGenerator;
        private readonly TimeOfDayProvider timeOfDayProvider;

        public DebugLogger(IRequestIdGenerator requestIdGenerator, TimeOfDayProvider timeOfDayProvider)
        {
            this.requestIdGenerator = requestIdGenerator;
            this.timeOfDayProvider = timeOfDayProvider;
        }

        public void WriteLine(string message)
        {
            Debug.WriteLine(message + " " + requestIdGenerator.RequestId + " " + timeOfDayProvider.Current);
        }
    }
}
