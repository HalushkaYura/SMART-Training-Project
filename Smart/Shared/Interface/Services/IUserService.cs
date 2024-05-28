using TaskBoard.Core.DTOs.UserDTO;

namespace Smart.Shared.Interface.Services
{
    public interface IUserService
    {
        //Task UploadAvatar(UserImageUploadDTO imageDTO, string userId);
        Task<string> GetUserImageAsync(string userId);
        Task<UserInfoDTO> GetUserByIdAsync(string userId);
    }
}
