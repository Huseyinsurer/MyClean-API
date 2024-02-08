using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Birds.Commands
{
    [TestFixture]
    public class AddBirdCommandHandlerTests
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
        public async Task AddBirdCommandHandler_ShouldAddBird()
        {
            // Arrange
            var handler = new AddBirdCommandHandler(_dbContext);

            var birdDto = new BirdDto
            {
                Name = "TestBird",
                CanFly = true,
                Color = "Blue"
            };

            var command = new AddBirdCommand(birdDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(birdDto.Name, result.Name);
            Assert.AreEqual(birdDto.CanFly, result.CanFly);
            Assert.AreEqual(birdDto.Color, result.Color);
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
