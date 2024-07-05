using FluentValidation;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.RegisterRequestDTO.UserName)
                .NotEmpty()
                .WithMessage(REQUIRED_USERNAME_ERROR_MESSAGE);

            RuleFor(x => x.RegisterRequestDTO.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(REQUIRED_VALID_EMAIL_ERROR_MESSAGE);

            RuleFor(x => x.RegisterRequestDTO.Password)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage(PASSWORD_MINIMUM_LENGTH_ERROR_MESSAGE)
                .Matches(COMPLEX_PASSWORD_REGEX)
                .WithMessage(COMPLEX_PASSWORD_REQUIREMENTS_NOT_MET_ERROR_MESSAGE);

            RuleFor(x => x.RegisterRequestDTO.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.RegisterRequestDTO.Password);
        }
    }
}
