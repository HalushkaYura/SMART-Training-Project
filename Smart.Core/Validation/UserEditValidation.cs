using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Validation
{
    public class UserEditValidation : AbstractValidator<UserEditDTO>
    {
        protected readonly UserManager<User> _userManager;
        public UserEditValidation(UserManager<User> manager)
        {
            _userManager = manager;

            RuleFor(user => user.Firstname)
                .NotEmpty()
                .Length(3, 50);


            RuleFor(user => user.Lastname)
                .NotEmpty()
                .Length(3, 50);


            //RuleFor(user => user.Email)
            //    .NotEmpty()
            //    .Must(IsUniqueEmail).WithMessage("{PropertyName} already exists.");
        }

        private bool IsUniqueEmail(string email)
        {
            var userObject = _userManager.FindByNameAsync(email).Result;
            return userObject == null;
        }

    }
}
