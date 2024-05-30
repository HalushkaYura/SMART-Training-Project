using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Validation
{
    public class UserRegistrationValidation : AbstractValidator<UserRegistrationDTO>
    {
        protected readonly UserManager<User> _userManager;

        public UserRegistrationValidation(UserManager<User> manager)
        {
            _userManager = manager;

            RuleFor(user => user.Firstname)
                .NotNull()
                .Length(3, 50);


            RuleFor(user => user.Lastname)
                .NotNull()
                .Length(3, 50);


            RuleFor(user => user.Email)
                .NotNull()
                .EmailAddress()
                .Must(IsUniqueUserEmail).WithMessage("{PropertyName} already exists.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("{PropertyName} must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("{PropertyName} must contain one or more special characters.")
                .Matches("^[^£# “”]*$").WithMessage("{PropertyName} must not contain the following characters £ # “” or spaces.");

        }

        private bool IsUniqueUserEmail(string email)
        {
            var userObject = _userManager.FindByEmailAsync(email).Result;
            return userObject == null;
        }

    }
}
