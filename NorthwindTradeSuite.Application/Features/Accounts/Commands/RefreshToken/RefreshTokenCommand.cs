using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Accounts;
using NorthwindTradeSuite.DTOs.Responses.Accounts;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(RefreshTokenRequestDTO RefreshTokenRequestDTO) : ICommand<Result<RefreshTokenResponseDTO>>;
}
