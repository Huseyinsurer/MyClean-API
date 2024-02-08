using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Application.Dtos;
using Infrastructure.Database;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly ApiMainContext _context;

        public AddBirdCommandHandler(ApiMainContext context)
        {
            _context = context;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            // Skapa en ny Bird från BirdDto
            Bird birdToAdd = new()
            {
                // Anpassa dessa egenskaper baserat på ditt BirdDto
                Name = request.NewBird.Name,
                CanFly = request.NewBird.CanFly,
                Color = request.NewBird.Color
                // ... andra egenskaper
            };

            // Lägg till fågeln i context och spara ändringar i databasen
            _context.Birds.Add(birdToAdd);
            await _context.SaveChangesAsync(cancellationToken);

            // Returnera den nyskapade fågeln
            return birdToAdd;
        }
    }
}

