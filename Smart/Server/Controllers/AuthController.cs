using Microsoft.AspNetCore.Mvc;
using ServiceStack;


namespace Smart.Server.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("IsAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("UserIdAuthenticated")]
        public IActionResult UserIdAuthenticated()
        {
            return Ok(User.Identity.GetId());
        }
    }
}
