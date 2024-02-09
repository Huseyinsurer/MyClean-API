using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.RegisterUser;
using Application.Dtos;
using Domain.Models.User;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

[TestFixture]
public class RegisterUserCommandHandlerTests
{
    [Test]
    public async Task Handle_ValidUser_ReturnsRegisteredUser()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        var dbContext = new ApiMainContext(dbContextOptions);
        var handler = new RegisterUserCommandHandler(dbContext);

        var newUserDto = new UserDto
        {
            Username = "newuser",
            Userpassword = "newpassword"
        };

        var command = new RegisterUserCommand(newUserDto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(newUserDto.Username, result.Username);
        Assert.AreEqual(newUserDto.Userpassword, result.Userpassword);

        // Check if the user is saved in the database
        var userInDatabase = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == result.Id);
        Assert.NotNull(userInDatabase);
        Assert.AreEqual(newUserDto.Username, userInDatabase.Username);
        Assert.AreEqual(newUserDto.Userpassword, userInDatabase.Userpassword);
    }
}

