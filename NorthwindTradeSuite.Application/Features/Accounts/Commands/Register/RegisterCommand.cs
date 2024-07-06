using MediatR;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Accounts;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Register
{
    public sealed record RegisterCommand(RegisterRequestDTO RegisterRequestDTO) : ICommand<Result>;

    //public class RegisterCommand : IRequest<Result>
    //{
    //    public string UserName { get; set; } = null!;

    //    public string Email { get; set; } = null!;

    //    public string Password { get; set; } = null!;

    //    public string ConfirmPassword { get; set; } = null!;
    //}
}
