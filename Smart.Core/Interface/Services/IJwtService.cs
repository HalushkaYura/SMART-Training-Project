using Google.Apis.Auth;
using Smart.Core.Entities;
using Smart.Shared.DTOs.UserDTO;
using System.Security.Claims;

namespace Smart.Core.Interface.Services
{
    public interface IJwtService
    {
        IEnumerable<Claim> SetClaims(User user);
        string CreateToken(IEnumerable<Claim> claims);
        string CreateRefreshToken();
        IEnumerable<Claim> GetClaimsFromExpiredToken(string token);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(UserExternalAuthDTO authDTO);

    }
}
