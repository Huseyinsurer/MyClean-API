using NUnit.Framework;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Login;
using Application.Dtos;
using Domain.Models.User;
using Application.Commands.Users.LoginUser;
using Application.Validators.Users;
using Infrastructure.Repositories;

[TestFixture]
public class LoginUserValid
{
    [Test]
    public async Task Handle_ValidLogin_ReturnsLoginResponse()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var validator = new LoginUserCommandValidator();

        var handler = new LoginUserCommandHandler(userRepositoryMock.Object);
        var loginUserDto = new UserDto
        {
            Username = "testuser",
            Userpassword = "testpassword"
        };

        var command = new LoginUserCommand(loginUserDto);

        userRepositoryMock.Setup(u => u.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync(new UserModel
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Userpassword = "testpassword"
            });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsTrue(result.IsSuccessful);
        Assert.NotNull(result.UserId);

        userRepositoryMock.Verify(u => u.GetUserByUsername(It.Is<string>(username => username == loginUserDto.Username)), Times.Once);
    }
}
