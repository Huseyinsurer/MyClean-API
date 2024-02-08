using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class GetAllDogsQueryHandlerTests
{
    [Test]
    public async Task GetAllDogsQueryHandler_ShouldReturnAllDogs()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var handler = new GetAllDogsQueryHandler(dbContext);

        var dogsToAdd = new List<Dog>
        {
            new Dog { Id = Guid.NewGuid(), Name = "Dog1", Breed = "Breed1", Weight = 10 },
            new Dog { Id = Guid.NewGuid(), Name = "Dog2", Breed = "Breed2", Weight = 15 },
            new Dog { Id = Guid.NewGuid(), Name = "Dog3", Breed = "Breed3", Weight = 20 }
        };

        dbContext.Dogs.AddRange(dogsToAdd);
        await dbContext.SaveChangesAsync();

        var query = new GetAllDogsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(dogsToAdd.Count, result.Count);

        foreach (var dog in dogsToAdd)
        {
            Assert.IsTrue(result.Any(d => d.Id == dog.Id && d.Name == dog.Name && d.Breed == dog.Breed && d.Weight == dog.Weight));
        }
    }

    private ApiMainContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        return new ApiMainContext(options);
    }
}
