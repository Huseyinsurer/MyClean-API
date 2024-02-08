
using MediatR;
using Application.Dtos;
using Domain.Models;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdCommand : IRequest<Bird>
    {
        public Guid BirdId { get; }
        public BirdDto UpdatedBird { get; }

        public UpdateBirdCommand(Guid birdId, BirdDto updatedBird)
        {
            BirdId = birdId;
            UpdatedBird = updatedBird;
        }
    }
}
