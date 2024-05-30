using FluentValidation;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Validation
{
    public class UserLogValidation : AbstractValidator<UserLoginDTO>
    {
        public UserLogValidation()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
