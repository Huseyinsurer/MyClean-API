using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, bool>
    {
        private readonly ApiMainContext _dbContext;

        public DeleteBirdCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var birdToRemove = _dbContext.Birds.FirstOrDefault(b => b.Id == request.BirdId);

                if (birdToRemove != null)
                {
                    _dbContext.Birds.Remove(birdToRemove);

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch
            {
                // Handle exceptions if needed
                return false;
            }
        }
    }
}
