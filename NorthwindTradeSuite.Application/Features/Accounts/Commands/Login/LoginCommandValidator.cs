using FluentValidation;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.LoginRequestDTO.UserName)
                .NotEmpty()
                .WithMessage(REQUIRED_USERNAME_ERROR_MESSAGE);

            RuleFor(x => x.LoginRequestDTO.Password)
                .NotEmpty()
                .WithMessage(REQUIRED_PASSWORD_ERROR_MESSAGE);
        }
    }
}
