using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Birds.DeleteBird;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Birds.Commands
{
    [TestFixture]
    public class DeleteBirdCommandHandlerTests
    {
        private ApiMainContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApiMainContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new ApiMainContext(options);
        }

        [Test]
        public async Task DeleteBirdCommandHandler_ShouldDeleteBird()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            var handler = new DeleteBirdCommandHandler(_dbContext);

            // Add a bird to the database for testing deletion
            var birdToAdd = new Bird { Id = birdId, Name = "TestBird" };
            _dbContext.Birds.Add(birdToAdd);
            await _dbContext.SaveChangesAsync();

            var command = new DeleteBirdCommand(birdId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            Assert.IsNull(_dbContext.Birds.FirstOrDefault(b => b.Id == birdId));
        }

        // Add more tests as needed

        [TearDown]
        public void TearDown()
        {
            // Clean up resources, close database connection, etc.
            _dbContext.Dispose();
        }
    }
}

