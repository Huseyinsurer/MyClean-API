using Application.Commands.Users.Login;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserDto loginUser)
        {
            var command = new LoginUserCommand(loginUser);
            var result = await _mediator.Send(command);

            if (!string.IsNullOrEmpty(result.Token)) // Check if Token is not empty
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

