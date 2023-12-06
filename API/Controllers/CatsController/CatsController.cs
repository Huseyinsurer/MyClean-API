
using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Application.Commands.Cats.UpdateCat;

namespace API.Controllers.Cats
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public CatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all cats from the database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Create a new cat 
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));
        }

        // Delete a specific cat by Id
        [HttpDelete("deleteCat/{catId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCat(Guid catId)
        {
            var command = new DeleteCatCommand(catId);
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

