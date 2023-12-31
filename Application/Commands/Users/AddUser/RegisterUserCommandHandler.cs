using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.Models.User;
using Application.Dtos;
using Infrastructure.Database;

namespace Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserModel>
    {
        private readonly IMockDatabase _mockDatabase;

        public RegisterUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task<UserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new UserModel
            {
                // Kopiera över egenskaper från UserDto till UserModel
                // Exempel: Name = request.NewUser.Name,
            };

            // Sparar den nya användaren i din databas
            // _mockDatabase.Users.Add(newUser);

            // Utför andra nödvändiga åtgärder för att spara användaren

            // Returnera den skapade UserModel
            return newUser;
        }
    }
}

