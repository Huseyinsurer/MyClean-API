using Domain.Models;
using MediatR;

namespace Application.Queries.Dogs.GetAll
{
    public class GetAllDogsQuery : IRequest<List<Dog>>
    {
        public string Breed { get; set; } = string.Empty;
        public int? Weight { get; set; }
    }
}
