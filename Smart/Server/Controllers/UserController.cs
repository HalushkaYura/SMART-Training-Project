using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.UserDTO;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditUserDateAsync([FromBody] UserEditDTO userEditDTO)
        {
            await _userService.EditUserDateAsync(userEditDTO, UserId);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> UserPersonalIngoAsync()
        {
            var userInfo = await _userService.UserInfoAsync(UserId);

            return Ok(userInfo);
        }
        ////////////////////////////////////////////////////////////////////////////////


        [Authorize]
        [HttpPut]
        [Route("upload-foto")]
        public async Task<IActionResult> UploadUserImage([FromForm] UserImageUploadDTO imageDTO)
        {
            await _userService.UploadAvatar(imageDTO, UserId);
            return Ok();

        }
        [Authorize]
        [HttpGet]
        [Route("get-image/{email}")]
        public async Task<IActionResult> GetUserImage(string email)
        {
            var imageUrl = await _userService.GetUserImageAsync(email);
            return Ok(imageUrl);
        }
        [Authorize]
        [HttpGet]
        [Route("getAllUserForProject/{projectId}")]
        public async Task<IActionResult> GetUserImage(int projectId)
        {
            var userForProject = await _userService.GetAllUserProjectAsync(projectId);
            return Ok(userForProject);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUserAccount()
        {
            await _userService.DeleteUserAccount(UserId);

            return Ok();
        }

    }
}
