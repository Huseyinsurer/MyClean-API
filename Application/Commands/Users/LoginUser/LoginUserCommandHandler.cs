using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Login;
using Application.Dtos;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        private readonly ApiMainContext _dbContext;

        public LoginUserCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Hämta användaren från databasen (mockad här)
            var userFromDatabase = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == request.LoginUser.Username, cancellationToken);

            if (userFromDatabase != null && userFromDatabase.Userpassword == request.LoginUser.Userpassword)
            {
                // Inloggningen lyckades
                return new LoginResponse
                {
                    IsSuccessful = true,
                    UserId = userFromDatabase.Id
                };
            }

            // Inloggningen misslyckades
            return new LoginResponse
            {
                IsSuccessful = false,
                UserId = Guid.Empty // Använd ett standardvärde för misslyckade inloggningar
            };
        }
    }
}

