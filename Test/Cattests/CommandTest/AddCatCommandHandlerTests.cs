using NUnit.Framework;
using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class AddCatCommandHandlerTests
    {
        private AddCatCommandHandler _addCatCommandHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _addCatCommandHandler = new AddCatCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidCatDto_ReturnsCreatedCat()
        {
            // Arrange
            var newCatDto = new CatDto
            {
                Name = "Husko",
                LikesToPlay = true,
                Breed = "Persian",
                Weight = 85
                // Add other properties...
            };

            var command = new AddCatCommand(newCatDto);

            // Act
            var result = await _addCatCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCatDto.Name, result.Name);
            Assert.AreEqual(newCatDto.LikesToPlay, result.LikesToPlay);
            Assert.AreEqual(newCatDto.Breed, result.Breed);
            Assert.AreEqual(newCatDto.Weight, result.Weight);

            // Extra assertion: Check that the cat has actually been added to the mock database
            var createdCat = _mockDatabase.Cats.FirstOrDefault(c => c.Id == result.Id);
            Assert.IsNotNull(createdCat, "The cat should be present in the database.");
            Assert.AreEqual(newCatDto.Name, createdCat.Name, "The cat's name should match.");
        }
    }
}
