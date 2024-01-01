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
        private readonly IMockDatabase _mockDatabase;

        public CreateUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
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

            // Save the new user to your database
            _mockDatabase.Users.Add(newUser);

            // Perform other necessary actions to save the user

            // Return the created UserModel
            return newUser;
        }
    }
}
