using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Application.Commands.Dogs.DeleteDog;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, bool>
    {
        private readonly ApiMainContext _dbContext;

        public DeleteDogCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogToRemove = await _dbContext.Dogs.FindAsync(request.DogId);

            if (dogToRemove != null)
            {
                _dbContext.Dogs.Remove(dogToRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
