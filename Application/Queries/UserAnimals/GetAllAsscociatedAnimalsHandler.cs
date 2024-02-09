using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.UserAnimals
{
    public class GetAllAssociatedAnimalsHandler : IRequestHandler<GetAllAssociatedAnimalsQuery, List<AssociatedAnimalDto>>
    {
        private readonly ApiMainContext _context;

        public GetAllAssociatedAnimalsHandler(ApiMainContext context)
        {
            _context = context;
        }

        public async Task<List<AssociatedAnimalDto>> Handle(GetAllAssociatedAnimalsQuery request, CancellationToken cancellationToken)
        {
            var allAssociatedAnimals = await _context.Ownerships
                .Select(o => new AssociatedAnimalDto
                {
                    AnimalId = o.AnimalId,
                    AnimalName = o.Animal.Name,
                    UserId = o.UserId.ToString(),
                })
                .ToListAsync(cancellationToken);

            return allAssociatedAnimals;
        }
    }
}
