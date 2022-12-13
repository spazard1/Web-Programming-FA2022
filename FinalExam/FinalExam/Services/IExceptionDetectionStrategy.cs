using System;

namespace FinalExam.Services
{
    public interface IExceptionDetectionStrategy
    {
        bool IsTransient(Exception ex);
    }
}
