
using MediatR;
using System;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatCommand : IRequest<bool>
    {
        public Guid CatId { get; }

        public DeleteCatCommand(Guid catId)
        {
            CatId = catId;
        }
    }
}
