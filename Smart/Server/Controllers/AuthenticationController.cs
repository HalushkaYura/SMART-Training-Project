

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Entities;
using Smart.Core.Interface.Services;
using Smart.Core.Roles;
using Smart.Shared.DTOs.UserDTO;
using System.Security.Claims;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] UserRegistrationDTO registrationDTO)
        {
            var user = new User()
            {
                UserName = registrationDTO.Email,
                Lastname = registrationDTO.Lastname,
                Firstname = registrationDTO.Firstname,
                Email = registrationDTO.Email,
                BirthDate = registrationDTO.BirthDay,
                ImageUrl = "userBase.png"
            };
            await _authenticationService.RegistrationAsync(user, registrationDTO.Password, SystemRoles.User);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            var tokens = await _authenticationService.LoginAsync(userLoginDTO.Email, userLoginDTO.Password);

            return Ok(tokens);
        }
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] UserAutorizationDTO userTokensDTO)
        {
            await _authenticationService.LogoutAsync(userTokensDTO);

            return NoContent();
        }


        [Authorize]
        [HttpGet]
        [Route("password/{email}")]
        public async Task<IActionResult> SentResetPasswordTokenAsync(string email)
        {
            await _authenticationService.SentResetPasswordTokenAsync(email);

            return Ok();
        }


        [Authorize]
        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] UserChangePasswordDTO userChangePasswordDTO)
        {
            await _authenticationService.ResetPasswordAsync(userChangePasswordDTO);

            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserSetNewPasswordDTO model)
        {
            await _authenticationService.ChangePasswordAsync(model, UserId);
            return Ok(new { Message = "Password changed successfully." });


        }


        [Authorize]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] UserAutorizationDTO userTokensDTO)
        {
            var tokens = await _authenticationService.RefreshTokenAsync(userTokensDTO);

            return Ok(tokens);
        }
        //----------------------------------------------------------------------------------------------------
        /*
        [Authorize]
        [HttpPost]
        [Route("signin-google")]
        public async Task<IActionResult> ExternalLoginAsync([FromBody] UserExternalAuthDTO authDTO)
        {
            var result = await _authenticationService.ExternalLoginAsync(authDTO);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("login-two-step")]
        public async Task<IActionResult> LoginTwoStepAsync([FromBody] UserTwoFactorDTO twoFactorDTO)
        {
            var tokens = await _authenticationService.LoginTwoStepAsync(twoFactorDTO);

            return Ok(tokens);
        }*/

    }
}
