using Application.Commands.Cats.DeleteCat;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class DeleteCatByIdTests
    {
        private DeleteCatCommandHandler _deleteCatCommandHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _deleteCatCommandHandler = new DeleteCatCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidId_DeletesCat()
        {
            // Arrange
            var catIdToDelete = new Guid("87654321-4321-8765-4321-987654321098");
            _mockDatabase.Cats.Add(new Cat { Id = catIdToDelete, Name = "TestCat" }); // Add a cat to the database for testing
            var command = new DeleteCatCommand(catIdToDelete);

            // Act
            var result = await _deleteCatCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);

            // Extra assertion: Check that the cat has actually been removed from the mock database
            var deletedCat = _mockDatabase.Cats.FirstOrDefault(c => c.Id == catIdToDelete);
            Assert.IsNull(deletedCat, "The cat should be removed from the database.");
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsFalse()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();
            var command = new DeleteCatCommand(invalidCatId);

            // Act
            var result = await _deleteCatCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
