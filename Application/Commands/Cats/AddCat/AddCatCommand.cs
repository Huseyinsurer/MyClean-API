// AddCatCommand
using MediatR;
using Application.Dtos;
using Domain.Models;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommand : IRequest<Cat>
    {
        public CatDto NewCat { get; }

        public AddCatCommand(CatDto newCat)
        {
            NewCat = newCat;
        }
    }
}

