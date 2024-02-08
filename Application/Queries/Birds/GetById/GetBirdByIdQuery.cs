using MediatR;
using Domain.Models;

namespace Application.Queries.Birds.GetBirdById
{
    public class GetBirdByIdQuery : IRequest<Bird>
    {
        public Guid BirdId { get; }
        public Guid Id { get; }

        public GetBirdByIdQuery(Guid birdId)
        {
            Id = birdId;
        }
    }
}