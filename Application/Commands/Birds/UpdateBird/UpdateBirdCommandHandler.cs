
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Infrastructure.Repositories;
using Domain.Models;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdCommandHandler : IRequestHandler<UpdateBirdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;

        public UpdateBirdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird> Handle(UpdateBirdCommand request, CancellationToken cancellationToken)
        {
            var existingBird = await _birdRepository.GetBirdById(request.BirdId);

            if (existingBird == null)
            {
                // Bird not found
                // Handle error or return null
                return null;
            }

            // Update the existing bird with the new information
           
            existingBird.CanFly = request.UpdatedBird.CanFly;
            existingBird.Color = request.UpdatedBird.Color;

            // Save the changes
            await _birdRepository.UpdateBird(existingBird);

            return existingBird;
        }
    }
}
