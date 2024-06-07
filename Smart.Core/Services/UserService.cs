﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Smart.Core.DTO.UserDTO;
using Smart.Core.Entities;
using Smart.Core.Helpers.Mails.ViewModels;
using Smart.Core.Helpers.Mails;
using Smart.Core.Helpers;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.UserDTO;
using Smart.Core.Resources;
using Smart.Core.Exeptions;

namespace Smart.Core.Services
{
    public class UserService : IUserService
    {
        protected readonly UserManager<User> _userManager;
        protected readonly IRepository<UserProject> _userProjectRepository;
        protected readonly IEmailSenderService _emailSenderService;
        protected readonly IMapper _mapper;
        private readonly IOptions<ImageSettings> _imageSettings;
        protected readonly ITemplateService _templateService;
        protected readonly IOptions<ClientUrl> _clientUrl;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserService(UserManager<User> userManager,
            IMapper mapper,
            IEmailSenderService emailSenderService,
            IOptions<ImageSettings> imageSettings,
            ITemplateService templateService,
            IOptions<ClientUrl> clientUrl,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IRepository<UserProject> userProjectRepository)
        {
            _userManager = userManager;
            //_inviteUserRepository = inviteUser;
            _mapper = mapper;
            _imageSettings = imageSettings;
            _emailSenderService = emailSenderService;
            _templateService = templateService;
            _clientUrl = clientUrl;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _userProjectRepository = userProjectRepository;
        }

        public async Task<UserChangeInfoDTO> UserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userPersonalInfo = _mapper.Map<UserChangeInfoDTO>(user);

            return userPersonalInfo;
        }



        //------------------------------   EditUserDate ---------------------------------------
        public async Task EditUserDateAsync(UserEditDTO userEditDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Якщо користувача не знайдено, можливо, ви можете виконати обробку помилки
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);
            }

            user.Firstname = userEditDTO.Firstname;
            user.Lastname = userEditDTO.Lastname;


            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new Exception($"Помилка при оновленні користувача: {string.Join(", ", errors)}");
            }
        }


        public async Task<bool> CheckIsTwoFactorVerificationAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (!user.EmailConfirmed)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest,
                ErrorMessages.EmailNotConfirm);
            }

            return await _userManager.GetTwoFactorEnabledAsync(user);
        }

        public async Task ChangeTwoFactorVerificationStatusAsync(string userId, UserChange2faStatusDTO statusDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var isUserToken = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", statusDTO.Token);

            if (!isUserToken)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.Invalid2FVCode);
            }

            var result = await _userManager.SetTwoFactorEnabledAsync(user, !await _userManager.GetTwoFactorEnabledAsync(user));

            if (!result.Succeeded)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.InvalidRequest);
            }

            await Task.CompletedTask;
        }

        public async Task SendTwoFactorCodeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Provis 2fa code",
                Body = await _templateService.GetTemplateHtmlAsStringAsync("Mails/TwoFactorCode",
                    new UserToken() { Token = twoFactorToken, UserName = user.UserName, Uri = _clientUrl.Value.ApplicationUrl })
            };

            await _emailSenderService.SendEmailAsync(message);

            await Task.CompletedTask;
        }

        public async Task SetPasswordAsync(string userId, UserSetPasswordDTO userSetPasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (await _userManager.HasPasswordAsync(user))
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.PasswordIsExist);
            }

            await _userManager.AddPasswordAsync(user, userSetPasswordDTO.Password);
        }

        public async Task<bool> IsHavePasswordAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.HasPasswordAsync(user);
        }

        //-----------------------------------------------------------------------------------

        public async Task UploadAvatar(UserImageUploadDTO imageDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);

            if (imageDTO.Image != null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/users");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageDTO.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageDTO.Image.CopyToAsync(fileStream);
                }
                user.ImageUrl = uniqueFileName;
                await _userManager.UpdateAsync(user);
            }
        }
        public async Task<string> GetUserImageAsync(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);

            var imageUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/images/users/{user.ImageUrl}";

            if (!System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "images/users", user.ImageUrl)))
            {
                imageUrl = string.Empty;
            }

            return imageUrl;
        }

        /* public async Task<byte[]> GetUserImageAsync(string email)
         {
             var user = await _userManager.FindByEmailAsync(email);
             if (user == null)
             {
                 throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);
             }

             var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/users", user.ImageUrl);

             if (!System.IO.File.Exists(imagePath))
             {
                 return new byte[0]; // Повертаємо порожній масив
             }

             return await System.IO.File.ReadAllBytesAsync(imagePath);
         }*/

        public async Task DeleteUserAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);
            var userList = await _userProjectRepository.GetListAsync(x => x.UserId == userId);
            if (userList.Any())
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "In order to delete the account, you need to exit all projects");
            await _userManager.DeleteAsync(user);
        }

    }
}
