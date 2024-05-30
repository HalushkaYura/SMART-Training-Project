using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("IsAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }
    }
}
