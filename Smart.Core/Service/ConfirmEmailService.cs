﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Smart.Core.Entities;
using Smart.Core.Exeptions;
using Smart.Core.Helpers.Mails;
using Smart.Core.Helpers.Mails.ViewModels;
using Smart.Core.Interface.Services;
using Smart.Core.Resources;
using Smart.Shared.DTOs.UserDTO;
using System.Text;

namespace Smart.Core.Service
{
    public class ConfirmEmailService : IConfirmEmailService
    {
        protected readonly UserManager<User> _userManager;
        protected readonly IEmailSenderService _emailService;
        protected readonly ITemplateService _templateService;
        protected readonly ClientUrl _clientUrl;

        public ConfirmEmailService(UserManager<User> userManager,
            IEmailSenderService emailSender,
            ITemplateService templateService,
            IOptions<ClientUrl> options)
        {
            _userManager = userManager;
            _emailService = emailSender;
            _templateService = templateService;
            _clientUrl = options.Value;
        }

        public async Task SendConfirmMailAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            CheckUserAndEmailConfirmed(user);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = Convert.ToBase64String(Encoding.Unicode.GetBytes(token));

            await _emailService.SendEmailAsync(new MailRequest()
            {
                ToEmail = user.Email,
                Subject = "Moneyboard Confirm Email",
                Body = await _templateService.GetTemplateHtmlAsStringAsync("Mails/ConfirmEmail",
                    new UserToken() { Token = encodedCode, UserName = user.UserName, Uri = _clientUrl.ApplicationUrl })
            });

            await Task.CompletedTask;
        }

        public async Task ConfirmEmailAsync(string userId, UserConfirmEmailDTO confirmEmailDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);

            CheckUserAndEmailConfirmed(user);

            var decodedCode = DecodeUnicodeBase64(confirmEmailDTO.ConfirmationCode);

            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

            if (!result.Succeeded)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages.ConfirmEmailInvalidCode);
            }

            await _userManager.UpdateSecurityStampAsync(user);

            await Task.CompletedTask;
        }

        private void CheckUserAndEmailConfirmed(User user)
        {
            user.UserNullChecking();

            if (user.EmailConfirmed)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages.EmailNotConfirm);
            }
        }

        public string DecodeUnicodeBase64(string input)
        {
            var bytes = new Span<byte>(new byte[input.Length]);

            if (!Convert.TryFromBase64String(input, bytes, out var bytesWritten))
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages.ConfirmEmailInvalidCode);
            }

            return Encoding.Unicode.GetString(bytes.Slice(0, bytesWritten));
        }
    }
}
