using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Portal.Services;

namespace Portal.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly ISecurityProvider securityProvider;

        public AuthorizationFilter(ISecurityProvider securityProvider)
        {
            this.securityProvider = securityProvider;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizationHeader))
            {
                // response not authorized status code, 401
                return;
            }

            var authorization = authorizationHeader.ToString();

            if (!authorization.StartsWith("Bearer "))
            {
                // response not authorized status code 401
                return;
            }

            // 7 because "Bearer " is seven characters with the space.
            // this is substring
            authorization = authorization[7..];

            if (!this.securityProvider.ValidateToken(authorization))
            {
                // response not authorized status code 401
            }
        }
    }
}
