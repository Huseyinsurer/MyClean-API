using Application.Commands.Dogs;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs
{
    internal sealed class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;

        public AddDogCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                Breed = request.NewDog.Breed,
                Weight = request.NewDog.Weight
            };

            _mockDatabase.Dogs.Add(dogToCreate);

            return Task.FromResult(dogToCreate);
        }
    }
}
