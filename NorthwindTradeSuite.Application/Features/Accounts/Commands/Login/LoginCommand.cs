using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Accounts;
using NorthwindTradeSuite.DTOs.Responses.Accounts;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Login
{
    public sealed record LoginCommand(LoginRequestDTO LoginRequestDTO) : ICommand<RequestResult<LoginResponseDTO>>;
}
