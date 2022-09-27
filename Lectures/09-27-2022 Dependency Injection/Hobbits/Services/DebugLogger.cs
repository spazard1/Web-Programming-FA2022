using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hobbits.Services
{
    public class DebugLogger : IHobbitLogger
    {
        private readonly RequestIdGenerator requestIdGenerator;
        private readonly TimeOfDayProvider timeOfDayProvider;

        public DebugLogger(RequestIdGenerator requestIdGenerator, TimeOfDayProvider timeOfDayProvider)
        {
            this.requestIdGenerator = requestIdGenerator;
            this.timeOfDayProvider = timeOfDayProvider;
        }

        public void Log(string message)
        {
            Debug.WriteLine(message + " " + requestIdGenerator.RequestId + " " + timeOfDayProvider.Current);
        }
    }
}
