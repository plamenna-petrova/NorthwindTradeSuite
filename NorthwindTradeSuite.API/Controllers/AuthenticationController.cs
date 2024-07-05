using MediatR;
using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.Application.Features.Accounts.Commands.Register;
using NorthwindTradeSuite.DTOs.Requests.Accounts;

namespace NorthwindTradeSuite.API.Controllers
{
    [Route("api/[controller]")]
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
            var result = await _mediator.Send(registerCommand);

            if (result.IsSuccess)
            {
                return Ok(new { Message = result.Errors.FirstOrDefault() });
            }

            return BadRequest(result.Errors);
        }
    }
}
