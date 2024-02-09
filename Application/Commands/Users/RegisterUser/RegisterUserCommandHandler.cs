using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;
using Application.Dtos;
using Domain.Models.User;

namespace Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserModel>
    {
        private readonly ApiMainContext _dbContext;

        public RegisterUserCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new UserModel
            {
                Id = Guid.NewGuid(),
                Username = request.NewUser.Username,
                Userpassword = request.NewUser.Userpassword,
                // Kopiera över andra egenskaper från UserDto till UserModel om det behövs
            };

            // Spara den nya användaren i din databas
            _dbContext.Users.Add(newUser);

            // Utför andra nödvändiga åtgärder för att spara användaren i din databas
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Returnera den skapade UserModel
            return newUser;
        }
    }
}

