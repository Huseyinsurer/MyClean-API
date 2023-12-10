using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogByIdTests
    {
        private DeleteDogCommandHandler _deleteDogCommandHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _deleteDogCommandHandler = new DeleteDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidId_DeletesDog()
        {
            // Arrange
            var dogIdToDelete = new Guid("87654321-4321-8765-4321-987654321098");
            _mockDatabase.Dogs.Add(new Dog { Id = dogIdToDelete, Name = "TestDog" }); // Add a dog to the database for testing
            var command = new DeleteDogCommand(dogIdToDelete);

            // Act
            var result = await _deleteDogCommandHandler.Handle(command, CancellationToken.None);

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
            var result = await _deleteDogCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
