using MediatR;
using Domain.Models;

namespace Application.Queries.Birds.GetAllBirds
{
    public class GetAllBirdsQuery : IRequest<List<Bird>>
    {
    }
}
