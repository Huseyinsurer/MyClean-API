using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    internal class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateDogByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            Dog dogToUpdate = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            dogToUpdate.Name = request.UpdatedDog.Name;
            dogToUpdate.Breed = request.UpdatedDog.Breed;
            dogToUpdate.Weight = request.UpdatedDog.Weight;

            return Task.FromResult(dogToUpdate);
        }
    }
}
