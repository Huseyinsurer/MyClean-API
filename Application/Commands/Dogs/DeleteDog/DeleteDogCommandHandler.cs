using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog; 



namespace Application.Commands.Dogs
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;

        public DeleteDogCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogToRemove = _mockDatabase.Dogs.FirstOrDefault(d => d.Id == request.DogId);
            if (dogToRemove != null)
            {
                _mockDatabase.Dogs.Remove(dogToRemove);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
