using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IMockDatabase _mockDatabase;

        public GetUserByIdQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Hämta användaren från databasen baserat på användar-ID
            var user = _mockDatabase.Users.FirstOrDefault(u => u.Id == request.UserId);

            return Task.FromResult(user);
        }
    }
}
