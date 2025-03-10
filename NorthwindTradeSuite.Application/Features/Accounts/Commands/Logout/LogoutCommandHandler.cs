﻿using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.Domain.Entities.Identity;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Logout
{
    public class LogoutCommandHandler : ICommandHandler<LogoutCommand, RequestResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<RequestResult> Handle(LogoutCommand logoutCommand, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return RequestResult.Success("Logged out successfully");
        }
    }
}
