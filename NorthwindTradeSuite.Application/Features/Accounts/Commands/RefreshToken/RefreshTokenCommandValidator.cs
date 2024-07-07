using FluentValidation;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshTokenRequestDTO.AccessToken)
                .NotEmpty()
                .WithMessage(REQUIRED_ACCESS_TOKEN_ERROR_MESSAGE);

            RuleFor(x => x.RefreshTokenRequestDTO.RefreshToken)
                .NotEmpty()
                .WithMessage(REQUIRED_REFRESH_TOKEN_ERROR_MESSAGE);
        }
    }
}
