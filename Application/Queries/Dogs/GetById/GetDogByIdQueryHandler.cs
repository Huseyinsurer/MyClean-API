using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Domain.Models;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly ApiMainContext _dbContext;

        public GetDogByIdQueryHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog wantedDog = await _dbContext.Dogs.FirstOrDefaultAsync(dog => dog.Id == request.Id);
            return wantedDog;
        }
    }
}
