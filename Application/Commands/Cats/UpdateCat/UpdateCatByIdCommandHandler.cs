using Application.Commands.Cats.UpdateCat;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Cats.UpdateCat
{
    internal class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateCatByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat catToUpdate = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.Id)!;

            catToUpdate.Name = request.UpdatedCat.Name;
            catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
            catToUpdate.Breed = request.UpdatedCat.Breed;
            catToUpdate.Weight = request.UpdatedCat.Weight;

            return Task.FromResult(catToUpdate);
        }
    }
}
