using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, bool>
    {
        private readonly ApiMainContext _dbContext;

        public DeleteCatCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToRemove = _dbContext.Cats.FirstOrDefault(c => c.Id == request.CatId);
            if (catToRemove != null)
            {
                _dbContext.Cats.Remove(catToRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}

