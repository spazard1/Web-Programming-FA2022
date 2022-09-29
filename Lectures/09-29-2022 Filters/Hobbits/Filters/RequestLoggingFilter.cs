using Hobbits.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hobbits.Filters
{
    public class RequestLoggingFilter : IActionFilter
    {
        private readonly IHobbitLogger hobbitLogger;

        public RequestLoggingFilter(IHobbitLogger hobbitLogger)
        {
            this.hobbitLogger = hobbitLogger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.hobbitLogger.WriteLine("Ending an action");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.hobbitLogger.WriteLine(context.HttpContext.Request.Method + "-" + context.HttpContext.Request.Path);
        }
    }
}
