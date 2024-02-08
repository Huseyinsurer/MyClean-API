
using MediatR;
using System;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdCommand : IRequest
    {
        public Guid BirdId { get; }

        public DeleteBirdCommand(Guid birdId)
        {
            BirdId = birdId;
        }
    }
}
