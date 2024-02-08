using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Domain.Models;

namespace Application.Queries.Dogs.GetAll
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly ApiMainContext _dbContext;

        public GetAllDogsQueryHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            List<Dog> allDogsFromDatabase = await _dbContext.Dogs.ToListAsync();
            return allDogsFromDatabase;
        }
    }
}

