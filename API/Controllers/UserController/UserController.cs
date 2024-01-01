using Application.Commands.Users.Login;
using Application.Commands.Users.RegisterUser;
using Application.Commands.Users.Delete;
using Application.Dtos;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto newUser)
    {
        try
        {
            var result = await _mediator.Send(new RegisterUserCommand(newUser));
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateUserException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserDto loginUser)
    {
        try
        {
            // Send the login command to MediatR
            var response = await _mediator.Send(new LoginUserCommand(loginUser));

            // Return the token and user ID
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserNotExistException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }

    }
    [HttpPut("updateUser/{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDto updatedUser)
    {
        try
        {
            var result = await _mediator.Send(new UpdateUserCommand(userId, updatedUser));
            return Ok(result);
        }
        catch (UserIdNotExistException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }
    [HttpDelete("deleteUser/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var result = await _mediator.Send(new DeleteUserCommand(userId));

            if (result.IsSuccess)
            {
                return Ok(new { IsSuccess = true, Message = result.Message });
            }
            else
            {
                return NotFound(new { IsSuccess = false, Message = result.Message });
            }
        }
        catch (UserIdNotExistException ex)
        {
            return NotFound(new { IsSuccess = false, Message = ex.Message });
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }
}
