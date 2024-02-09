using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Exceptions;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Application.Commands.Users.UpdateUser
{

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
{
    private readonly ApiMainContext _dbContext;

    public UpdateUserCommandHandler(ApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Hämta användaren från databasen
        var userToUpdate = await _dbContext.Users.FindAsync(request.UserId);

        if (userToUpdate == null)
        {
            // User not found
            throw new UserIdNotExistException(request.UserId);
        }

        // Uppdatera användaregenskaper
        userToUpdate.Username = request.UpdatedUser.Username;
        userToUpdate.Userpassword = request.UpdatedUser.Userpassword;

        // Spara ändringar i databasen
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Skapa och returnera UserModel baserat på den uppdaterade användaren
        var userModel = new UserModel
        {
            Id = userToUpdate.Id,
            Username = userToUpdate.Username,
            // ... andra egenskaper
        };

        return userModel;
    }
}
}