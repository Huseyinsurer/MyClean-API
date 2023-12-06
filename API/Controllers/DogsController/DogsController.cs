
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Application.Commands.Dogs;

namespace API.Controllers.Dogs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all dogs from the database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            return Ok(await _mediator.Send(new AddDogCommand(newDog)));
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));
        }

        // Delete a specific dog by Id
        [HttpDelete("deleteDog/{dogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDog(Guid dogId)
        {
            // Skicka med dogId när du skapar DeleteDogCommand
            var command = new DeleteDogCommand(dogId);
            var success = await _mediator.Send(command);

            if (success)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
