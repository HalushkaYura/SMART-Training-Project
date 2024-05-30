using FluentValidation;
using Smart.Core.DTO.UserDTO;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Validation
{
    public class UserTwoFactorLoginValidation : AbstractValidator<UserTwoFactorDTO>
    {
        public UserTwoFactorLoginValidation()
        {
            RuleFor(user => user.Token)
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.Provider)
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull();
        }
    }
}
