using Hobbits.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hobbits.Filters
{
    public class RequestIdFilter : IActionFilter
    {
        private readonly RequestIdGenerator requestIdGenerator;

        public RequestIdFilter(RequestIdGenerator requestIdGenerator)
        {
            this.requestIdGenerator = requestIdGenerator;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers["request-id"] = requestIdGenerator.RequestId;
        }
    }
}
