using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.UserAnimals
{
    public class GetAllAssociatedAnimalsQuery : IRequest<List<AssociatedAnimalDto>>
    {

    }

    public class AssociatedAnimalDto
    {
        public Guid AnimalId { get; set; }
        public string AnimalName { get; set; }
        public string UserId { get; set; }
    }
}
