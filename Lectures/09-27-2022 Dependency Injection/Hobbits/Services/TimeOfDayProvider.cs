using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hobbits.Services
{
    public class TimeOfDayProvider
    {
        public DateTime Current { get; } = DateTime.UtcNow;
    }
}
