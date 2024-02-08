
using MediatR;
using Application.Dtos;
using Domain.Models;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommand : IRequest<Bird>
    {
        public BirdDto NewBird { get; }

        public AddBirdCommand(BirdDto newBird)
        {
            NewBird = newBird;
        }
    }
}
