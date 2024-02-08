using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Repositories.Cats;


namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public AddCatCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            var newCat = new Cat
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                LikesToPlay = request.NewCat.LikesToPlay,
                Breed = request.NewCat.Breed,
                Weight = request.NewCat.Weight
                // Andra egenskaper...
            };

            await _catRepository.AddCat(newCat);

            return newCat;
        }
    }
}
