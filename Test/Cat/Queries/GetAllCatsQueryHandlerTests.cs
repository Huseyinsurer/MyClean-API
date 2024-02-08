using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Cats.Queries
{
    [TestFixture]
    public class GetAllCatsQueryHandlerTests
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
        public async Task GetAllCatsQueryHandler_ShouldReturnAllCats()
        {
            // Arrange
            var handler = new GetAllCatsQueryHandler(_dbContext);

            // Add some test cats to the in-memory database
            var catsToAdd = new List<Cat>
            {
                new Cat { Id = Guid.NewGuid(), Name = "Cat1", LikesToPlay = true, Breed = "Breed1", Weight = 5 },
                new Cat { Id = Guid.NewGuid(), Name = "Cat2", LikesToPlay = false, Breed = "Breed2", Weight = 8 },
                // Add more cats as needed
            };

            _dbContext.Cats.AddRange(catsToAdd);
            await _dbContext.SaveChangesAsync();

            var query = new GetAllCatsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(catsToAdd.Count, result.Count);

            foreach (var cat in catsToAdd)
            {
                Assert.IsTrue(result.Any(c => c.Id == cat.Id && c.Name == cat.Name && c.LikesToPlay == cat.LikesToPlay && c.Breed == cat.Breed && c.Weight == cat.Weight));
            }
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
