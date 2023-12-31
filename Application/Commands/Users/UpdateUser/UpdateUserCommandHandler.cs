using Application.Exceptions;

using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
{
    private readonly IMockDatabase _mockDatabase;

    public UpdateUserCommandHandler(IMockDatabase mockDatabase)
    {
        _mockDatabase = mockDatabase;
    }

    public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userToUpdate = _mockDatabase.Users.Find(user => user.Id == request.UserId);

        if (userToUpdate == null)
        {
            // User not found
            throw new UserIdNotExistException(request.UserId);
        }

        // Update user properties
        userToUpdate.Username = request.UpdatedUser.Username;
        userToUpdate.Userpassword = request.UpdatedUser.Userpassword;

        // Save changes (mock database does not have a SaveChanges method)
        // You might not need this line for your specific implementation

        return userToUpdate;
    }
}
