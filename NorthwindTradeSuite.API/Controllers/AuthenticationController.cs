using MediatR;
using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.Application.Features.Accounts.Commands.Login;
using NorthwindTradeSuite.Application.Features.Accounts.Commands.Logout;
using NorthwindTradeSuite.Application.Features.Accounts.Commands.RefreshToken;
using NorthwindTradeSuite.Application.Features.Accounts.Commands.Register;
using NorthwindTradeSuite.DTOs.Requests.Accounts;

namespace NorthwindTradeSuite.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var registerCommand = new RegisterCommand(registerRequestDTO);
            var registerResult = await _mediator.Send(registerCommand);

            if (registerResult.IsSuccess)
            {
                return Ok(new { Message = registerResult.Errors.FirstOrDefault() });
            }

            return BadRequest(registerResult.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginCommand = new LoginCommand(loginRequestDTO);
            var loginResult = await _mediator.Send(loginCommand);

            if (loginResult.IsSuccess)
            {
                return Ok(loginResult.Value);
            }

            return Unauthorized(new { Message = loginResult.Errors.FirstOrDefault() });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            var refreshTokenCommand = new RefreshTokenCommand(refreshTokenRequestDTO);
            var refreshTokenResult = await _mediator.Send(refreshTokenCommand);

            if (refreshTokenResult.IsSuccess)
            {
                return Ok(refreshTokenResult.Value);
            }

            return Unauthorized(new { Message = refreshTokenResult.Errors.FirstOrDefault() });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand logoutCommand)
        {
            var logoutResult = await _mediator.Send(logoutCommand);

            if (logoutResult.IsSuccess)
            {
                return Ok(new { Message = logoutResult.Errors.FirstOrDefault() });
            }

            return BadRequest(logoutResult.Errors);
        }
    }
}
