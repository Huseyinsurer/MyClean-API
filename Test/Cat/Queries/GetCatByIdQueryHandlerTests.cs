using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Cats.Queries
{
    [TestFixture]
    public class GetCatByIdQueryHandlerTests
    {
        [Test]
        public async Task GetCatByIdQueryHandler_ShouldReturnCatById()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApiMainContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var dbContext = new ApiMainContext(dbContextOptions))
            {
                var expectedCat = new Cat { Id = catId, Name = "TestCat", LikesToPlay = true, Breed = "Persian", Weight = 5 };
                dbContext.Cats.Add(expectedCat);
                await dbContext.SaveChangesAsync();

                var query = new GetCatByIdQuery(catId);
                var handler = new GetCatByIdQueryHandler(dbContext);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Id, Is.EqualTo(expectedCat.Id));
                Assert.That(result.Name, Is.EqualTo(expectedCat.Name));
                Assert.That(result.LikesToPlay, Is.EqualTo(expectedCat.LikesToPlay));
                Assert.That(result.Breed, Is.EqualTo(expectedCat.Breed));
                Assert.That(result.Weight, Is.EqualTo(expectedCat.Weight));
            }
        }

        [Test]
        public async Task GetCatByIdQueryHandler_ShouldReturnNullForNonexistentCat()
        {
            // Arrange
            var nonExistentCatId = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApiMainContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var dbContext = new ApiMainContext(dbContextOptions))
            {
                var query = new GetCatByIdQuery(nonExistentCatId);
                var handler = new GetCatByIdQueryHandler(dbContext);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.Null(result);
            }
        }
    }
}
