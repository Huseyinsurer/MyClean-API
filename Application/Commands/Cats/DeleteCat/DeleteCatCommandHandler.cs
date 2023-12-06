
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;

        public DeleteCatCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToRemove = _mockDatabase.Cats.FirstOrDefault(c => c.Id == request.CatId);
            if (catToRemove != null)
            {
                _mockDatabase.Cats.Remove(catToRemove);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
