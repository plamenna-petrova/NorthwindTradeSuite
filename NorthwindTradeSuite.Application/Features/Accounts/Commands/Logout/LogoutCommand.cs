using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Logout
{
    public sealed record LogoutCommand() : ICommand<RequestResult>;
}
