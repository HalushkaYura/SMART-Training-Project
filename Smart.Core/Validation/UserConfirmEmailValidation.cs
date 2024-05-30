using FluentValidation;
using Smart.Core.DTO.UserDTO;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Validation
{
    public class UserConfirmEmailValidation : AbstractValidator<UserConfirmEmailDTO>
    {
        public UserConfirmEmailValidation()
        {
            RuleFor(email => email.ConfirmationCode)
                .NotEmpty()
                .NotNull();
        }
    }
}
