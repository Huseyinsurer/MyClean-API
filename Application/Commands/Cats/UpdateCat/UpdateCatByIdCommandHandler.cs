using Application.Commands.Dogs.UpdateDog;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cats.UpdateCat
{
    internal class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateCatByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat CatToUpdate = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.Id)!;

            CatToUpdate.Name = request.UpdatedCat.Name;

            return Task.FromResult(CatToUpdate);
        }
    }
}
