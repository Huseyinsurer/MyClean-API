using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Birds.GetAllBirds;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Birds.Queries
{
    [TestFixture]
    public class GetAllBirdsQueryHandlerTests
    {
        private ApiMainContext _dbContext;
        private GetAllBirdsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApiMainContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new ApiMainContext(options);
            _handler = new GetAllBirdsQueryHandler(_dbContext);
        }

        [Test]
        public async Task Handle_GetAllBirds_ShouldReturnAllBirds()
        {
            // Arrange
            var birdsToAdd = new List<Bird>
            {
                new Bird { Name = "Bird1", CanFly = true, Color = "Blue" },
                new Bird { Name = "Bird2", CanFly = false, Color = "Red" },
                new Bird { Name = "Bird3", CanFly = true, Color = "Green" }
            };

            _dbContext.Birds.AddRange(birdsToAdd);
            await _dbContext.SaveChangesAsync();

            var query = new GetAllBirdsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Logga antalet fåglar som returnerats
            Console.WriteLine($"Number of birds returned: {result.Count}");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
