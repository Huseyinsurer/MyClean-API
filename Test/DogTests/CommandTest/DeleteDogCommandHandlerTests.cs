using NUnit.Framework;
using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Dogs.DeleteDog;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogCommandHandlerTests
    {
        private DeleteDogCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidId_DeletesDogFromDatabase()
        {
            // Arrange
            var dogIdToDelete = Guid.NewGuid();
            _mockDatabase.Dogs.Add(new Dog { Id = dogIdToDelete, Name = "Huskko" });

            var command = new DeleteDogCommand(dogIdToDelete);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);

            // Extra assertion: Check that the dog has actually been removed from the mock database
            var deletedDog = _mockDatabase.Dogs.FirstOrDefault(d => d.Id == dogIdToDelete);
            Assert.IsNull(deletedDog, "The dog should be removed from the database.");
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsFalse()
        {
            // Arrange
            var invalidDogId = Guid.NewGuid();
            var command = new DeleteDogCommand(invalidDogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
