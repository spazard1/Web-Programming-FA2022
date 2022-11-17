using Microsoft.AspNetCore.Mvc;
using Portal.Filters;

namespace Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(AuthorizationFilter))]
    public class GladosController : Controller
    {

        [HttpGet]
        public IActionResult Quote()
        {
            return new ContentResult()
            {
                Content = "This is a quote"
            };
        }
    }
}
