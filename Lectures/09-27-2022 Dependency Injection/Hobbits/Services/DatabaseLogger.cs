using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hobbits.Services
{
    public class DatabaseLogger : IHobbitLogger
    {
        private readonly RequestIdGenerator requestIdGenerator;

        public DatabaseLogger(RequestIdGenerator requestIdGenerator)
        {
            this.requestIdGenerator = requestIdGenerator;
        }

        public void Log(string message)
        {
            // pretend this logs to a database
        }
    }
}
