using MediatR;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Accounts;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Register
{
    public sealed record RegisterCommand(RegisterRequestDTO RegisterRequestDTO) : ICommand<Result>;
}
