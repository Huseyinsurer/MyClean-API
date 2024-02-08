using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly ApiMainContext _dbContext;

        public UpdateDogByIdCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToUpdate = await _dbContext.Dogs.FindAsync(request.Id);

            if (dogToUpdate != null)
            {
                dogToUpdate.Name = request.UpdatedDog.Name;
                dogToUpdate.Breed = request.UpdatedDog.Breed;
                dogToUpdate.Weight = request.UpdatedDog.Weight;

                await _dbContext.SaveChangesAsync();
            }

            return dogToUpdate;
        }
    }
}
