using FinalExam.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExam.Filters
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
            context.HttpContext.Response.Headers["request-id"] = this.requestIdGenerator.RequestId;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
