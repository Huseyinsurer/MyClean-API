using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Login;
using Application.Dtos;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Hämta användaren från databasen (mockad här)
            var userFromDatabase = await _userRepository.GetUserByUsername(request.LoginUser.Username);

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
