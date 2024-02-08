using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.Models.User;
using Application.Dtos;
using Infrastructure.Database;

namespace Application.Commands.Users.RegisterUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
    {
        private readonly ApiMainContext _dbContext;

        public CreateUserCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new UserModel
            {
                Id = Guid.NewGuid(),  // Generate a new unique ID for the user
                Username = request.NewUser.Username,
                Userpassword = request.NewUser.Userpassword,
                // Copy other properties from UserDto to UserModel
                // Example: Name = request.NewUser.Name,
            };

            // Save the new user to your database using EF Core
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return the created UserModel
            return newUser;
        }
    }
}
