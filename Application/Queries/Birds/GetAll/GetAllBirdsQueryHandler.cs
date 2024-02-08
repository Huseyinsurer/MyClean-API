using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Birds.GetAllBirds
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly ApiMainContext _context;

        public GetAllBirdsQueryHandler(ApiMainContext context)
        {
            _context = context;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            // Hämta alla Birds från databasen
            List<Bird> allBirds = await _context.Birds.ToListAsync(cancellationToken);

            // Returnera listan med fåglar
            return allBirds;
        }
    }
}
