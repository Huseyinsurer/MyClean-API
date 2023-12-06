using MediatR;
using System;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogCommand : IRequest<bool>
    {
        public Guid DogId { get; set; }

        public DeleteDogCommand(Guid dogId)
        {
            DogId = dogId;
        }
    }
}

