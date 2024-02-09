using Application.Commands.UserAnimals;
using Application.Dtos;
using Application.Queries.UserAnimals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserAnimalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserAnimalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("associateUserWithAnimal")]
    public async Task<IActionResult> AssociateUserWithAnimal([FromBody] UserAnimalDto userAnimal)
    {
        try
        {
            var result = await _mediator.Send(new AssociateUserWithAnimalCommand(userAnimal));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("disassociateUserFromAnimal")]
    public async Task<IActionResult> DisassociateUserFromAnimal([FromBody] UserAnimalDto userAnimal)
    {
        try
        {
            var result = await _mediator.Send(new DisassociateUserFromAnimalCommand(userAnimal));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAllAssociatedAnimals")]
    public async Task<IActionResult> GetAllAssociatedAnimals()
    {
        try
        {
            var result = await _mediator.Send(new GetAllAssociatedAnimalsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
