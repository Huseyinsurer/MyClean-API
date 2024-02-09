// DeleteUserCommandHandlerTests.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Application.Commands.Users.Delete;
using Infrastructure.Repositories;

[TestFixture]
public class DeleteUserCommandHandlerTests
{
    [Test]
    public async Task Handle_ValidUser_ReturnsDeleteUserResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var dbContext = new ApiMainContext(options))
        {
            var userId = Guid.NewGuid();
            var userToDelete = new Domain.Models.User.UserModel { Id = userId, Username = "testuser" };
            dbContext.Users.Add(userToDelete);
            dbContext.SaveChanges();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetUserById(userId)).ReturnsAsync(userToDelete);

            var handler = new DeleteUserCommandHandler(dbContext);
            var command = new DeleteUserCommand(userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Deletion was successful.", result.Message);
        }
    }
}
