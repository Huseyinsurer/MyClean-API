using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Repositories.Dogs;


namespace Application.Commands.Dogs.AddDog
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public AddDogCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            var newDog = new Dog
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                Breed = request.NewDog.Breed,
                Weight = request.NewDog.Weight
                // Andra egenskaper...
            };

            await _dogRepository.AddDog(newDog);

            return newDog;
        }
    }
}
