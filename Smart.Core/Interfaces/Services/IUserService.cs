﻿

using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Interfaces.Services
{
    public interface IUserService
    {
        //Task UploadAvatar(UserImageUploadDTO imageDTO, string userId);
        Task<string> GetUserImageAsync(string userId);
        Task<UserInfoDTO> GetUserByIdAsync(string userId);
    }
}