using Infrastructure.Database;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly ApiMainContext _dbContext;

        public UpdateCatByIdCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat catToUpdate = _dbContext.Cats.FirstOrDefault(cat => cat.Id == request.Id);

            if (catToUpdate != null)
            {
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
                catToUpdate.Breed = request.UpdatedCat.Breed;
                catToUpdate.Weight = request.UpdatedCat.Weight;

                await _dbContext.SaveChangesAsync();
            }

            return catToUpdate;
        }
    }
}
