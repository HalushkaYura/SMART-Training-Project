using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Smart.Server.Entities;
using Smart.Shared.Interface.Repository;
using Smart.Shared.Interface.Services;
using TaskBoard.Core.DTOs.UserDTO;

namespace Smart.Server.Service
{
    public class UserService : IUserService
    {
        //private readonly IRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Task<string> GetUserImageAsync(string userId)
        {
            throw new NotImplementedException();
        }


        public async Task<UserInfoDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserInfoDTO>(user);
        }

    }
}
