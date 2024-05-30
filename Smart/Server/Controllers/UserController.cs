using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interface.Services;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("userInfo{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var userInfo = await _userService.GetUserByIdAsync(userId);
                if (userInfo == null)
                {
                    return NotFound();
                }
                return Ok(userInfo);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Помилка сервера");
            }
        }
        [HttpGet]
        [Route("currentUser")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            try
            {
                // Отримуємо ідентифікатор поточного користувача з клеймом авторизації
                var userId = User.FindFirst("sub")?.Value;
                if (userId == null)
                {
                    return BadRequest("Ідентифікатор користувача не знайдено");
                }

                var userInfo = await _userService.GetUserByIdAsync(userId);
                if (userInfo == null)
                {
                    return NotFound();
                }

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Помилка сервера");
            }
        }

    }
}
