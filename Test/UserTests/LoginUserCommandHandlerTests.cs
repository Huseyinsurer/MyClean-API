using NUnit.Framework;
using Application.Commands.Users.Login;
using Application.Dtos;
using Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.LoginUser;

namespace Test.UserTests.CommandTest
{
    [TestFixture]
    public class LoginUserCommandHandlerTests
    {
        private LoginUserCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new LoginUserCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidLogin_ReturnsSuccessfulLoginResponse()
        {
            // Arrange
            var existingUserId = Guid.NewGuid();
            var existingUser = new Domain.Models.User.UserModel { Id = existingUserId, Username = "TestUser", Userpassword = "TestPassword" };
            _mockDatabase.Users.Add(existingUser);

            var loginUserDto = new UserDto { Username = "TestUser", Userpassword = "TestPassword" };
            var command = new LoginUserCommand(loginUserDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(existingUserId, result.UserId);
        }

        [Test]
        public async Task Handle_InvalidLogin_ReturnsFailedLoginResponse()
        {
            // Arrange
            var loginUserDto = new UserDto { Username = "NonExistentUser", Userpassword = "InvalidPassword" };
            var command = new LoginUserCommand(loginUserDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(Guid.Empty, result.UserId);
        }
    }
}
