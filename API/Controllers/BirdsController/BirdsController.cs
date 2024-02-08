using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAllBirds;
using Application.Queries.Birds.GetBirdById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : Controller
    {
        private readonly IMediator _mediator;

        public BirdsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all Birds
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            try
            {
                var query = new GetAllBirdsQuery();
                var result = await _mediator.Send(query);

                if (result is List<Bird> Birds && Birds.Any())
                {
                    return Ok(Birds);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GetBirdById
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            try
            {
                var query = new GetBirdByIdQuery(birdId);
                var bird = await _mediator.Send(query);
                return bird != null ? Ok(bird) : NotFound($"No bird found with ID: {birdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetBirdById: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update a bird
        [HttpPut]
        [Route("updateBird/{birdId}")]
        public async Task<IActionResult> UpdateBird(Guid birdId, [FromBody] BirdDto updatedBird)
        {
            try
            {
                var command = new UpdateBirdCommand(birdId, updatedBird);
                var result = await _mediator.Send(command);

                return result != null ? Ok(result) : NotFound($"No bird found with ID: {birdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateBird: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }



        // Delete a bird by id
        [HttpDelete]
        [Route("deleteBirdById/{birdId}")]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            try
            {
                var query = new DeleteBirdCommand(birdId);
                var bird = await _mediator.Send(query);
                return bird != null ? Ok(bird) : NotFound($"No bird found with ID: {birdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteBirdById: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Add a new Bird
        [HttpPost]
        [Route("addBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto birdDto)
        {
            try
            {
                var command = new AddBirdCommand(birdDto);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

