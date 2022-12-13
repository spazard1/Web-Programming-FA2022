using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using FinalExam.Services;

namespace FinalExam.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {

        private ISecurityProvider securityProvider;

        public AuthorizationFilter(ISecurityProvider securityProvider)
        {
            this.securityProvider = securityProvider;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("authorization", out StringValues authHeader))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }
            var authorization = authHeader.ToString();

            if (!authorization.StartsWith("Bearer "))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
                return;
            }

            authorization = authorization.Substring(7); // peel off the bearer

            if (!securityProvider.ValidateToken(authorization))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
                return;
            }
        }
    }
}
