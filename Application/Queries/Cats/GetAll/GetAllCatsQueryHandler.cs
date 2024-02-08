using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Domain.Models;

namespace Application.Queries.Cats.GetAll
{
    internal sealed class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly ApiMainContext _dbContext;

        public GetAllCatsQueryHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            List<Cat> allCatsFromDatabase = await _dbContext.Cats.ToListAsync();
            return allCatsFromDatabase;
        }
    }
}
