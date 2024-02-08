using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Domain.Models.User;

namespace Application.Queries.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly ApiMainContext _dbContext;

        public GetUserByIdQueryHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Hämta användaren från databasen baserat på användar-ID
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            return user;
        }
    }
}

