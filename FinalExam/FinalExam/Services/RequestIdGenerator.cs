using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExam.Services
{
    public class RequestIdGenerator
    {
        public string RequestId { get; }

        public RequestIdGenerator()
        {
            this.RequestId = Guid.NewGuid().ToString();
        }
    }
}
