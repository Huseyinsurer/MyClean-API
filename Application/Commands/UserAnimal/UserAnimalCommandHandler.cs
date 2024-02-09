using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.UserAnimals
{
    public class AssociateUserWithAnimalCommandHandler : IRequestHandler<AssociateUserWithAnimalCommand, bool>
    {
        private readonly IUserAnimalRepository _userAnimalRepository;

        public AssociateUserWithAnimalCommandHandler(IUserAnimalRepository userAnimalRepository)
        {
            _userAnimalRepository = userAnimalRepository;
        }

        public async Task<bool> Handle(AssociateUserWithAnimalCommand request, CancellationToken cancellationToken)
        {
            // Hämta UserAnimalDto från kommandot
            var userAnimalDto = request.UserAnimal;

            // Skapa en Ownership-entitet från UserAnimalDto
            var userAnimal = new Ownership
            {
                UserId = userAnimalDto.UserId,
                AnimalId = userAnimalDto.AnimalId,
                // Lägg till andra egenskaper här...
            };

            // Anropa repository för att associera användaren med djuret
            var result = await _userAnimalRepository.AssociateUserWithAnimal(userAnimal);

            return result;
        }
    }

    public class DisassociateUserFromAnimalCommandHandler : IRequestHandler<DisassociateUserFromAnimalCommand, bool>
    {
        private readonly IUserAnimalRepository _userAnimalRepository;

        public DisassociateUserFromAnimalCommandHandler(IUserAnimalRepository userAnimalRepository)
        {
            _userAnimalRepository = userAnimalRepository;
        }

        public async Task<bool> Handle(DisassociateUserFromAnimalCommand request, CancellationToken cancellationToken)
        {
            // Hämta UserAnimalDto från kommandot
            var userAnimalDto = request.UserAnimal;

            // Skapa en UserAnimal-entitet från UserAnimalDto
            var userAnimal = new Ownership
            {
                UserId = userAnimalDto.UserId,
                AnimalId = userAnimalDto.AnimalId,
                // Lägg till andra egenskaper här...
            };

            // Anropa repository för att disassociera användaren från djuret
            var result = await _userAnimalRepository.DisassociateUserFromAnimal(userAnimal);

            return result;
        }
    }
}
