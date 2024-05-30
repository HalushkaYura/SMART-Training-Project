using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Services
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
            UserInfoDTO userInfo = new UserInfoDTO
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                BirthDate = user.BirthDate,
            };
            //return _mapper.Map<UserInfoDTO>(user);
            return userInfo;
        }

    }
}
