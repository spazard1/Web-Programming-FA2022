using System;
using System.Net;

namespace FinalExam.Services
{
    public class ExceptionDetectionStrategy : IExceptionDetectionStrategy
    {
        public bool IsTransient(Exception ex)
        {
            var webException = ex as WebException;
            var response = webException?.Response as HttpWebResponse;
            return response?.StatusCode == HttpStatusCode.ServiceUnavailable;
        }
    }
}
