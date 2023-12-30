// LoginUserCommandHandler.cs
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Login;
using Application.Dtos;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        private readonly IMockDatabase _mockDatabase;

        public LoginUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase ?? throw new ArgumentNullException(nameof(mockDatabase));
        }

        public Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Hämta användaren från databasen (mockad här)
            var userFromDatabase = _mockDatabase.Users.FirstOrDefault(u => u.Username == request.LoginUser.Username);

            if (userFromDatabase != null && userFromDatabase.Userpassword == request.LoginUser.Userpassword)
            {
                // Inloggningen lyckades
                return Task.FromResult(new LoginResponse
                {
                    IsSuccessful = true,
                    UserId = userFromDatabase.Id
                });
            }

            // Inloggningen misslyckades
            return Task.FromResult(new LoginResponse
            {
                IsSuccessful = false,
                UserId = Guid.Empty // Använd ett standardvärde för misslyckade inloggningar
            });
        }
    }
}
