using Microsoft.AspNetCore.Mvc;
using Portal.Services;
using System.Security.Claims;

namespace Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ISecurityProvider securityProvider;

        public LoginController(ISecurityProvider securityProvider)
        {
            this.securityProvider = securityProvider;
        }

        /*
         * This will be a POST on yoru assignment
         */
        [HttpGet]
        public IActionResult Login()
        {
            var claims = new List<Claim>() { new Claim("username", "yacste")};

            var token = this.securityProvider.GetToken(claims);

            return new ContentResult()
            {
                Content = token
            };
        }
    }
}
