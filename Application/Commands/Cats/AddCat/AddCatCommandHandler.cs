using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Domain.Models;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly IMockDatabase _mockDatabase;

        public AddCatCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
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

            _mockDatabase.Cats.Add(newCat);

            return Task.FromResult(newCat);
        }
    }
}

