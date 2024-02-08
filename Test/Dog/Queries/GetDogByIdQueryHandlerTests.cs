using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class GetDogByIdQueryHandlerTests
{
    [Test]
    public async Task GetDogByIdQueryHandler_ShouldReturnDogById()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var handler = new GetDogByIdQueryHandler(dbContext);

        var dogId = Guid.NewGuid();
        var dogToAdd = new Dog { Id = dogId, Name = "TestDog", Breed = "Breed1", Weight = 15 };
        dbContext.Dogs.Add(dogToAdd);
        await dbContext.SaveChangesAsync();

        var query = new GetDogByIdQuery(dogId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(dogId, result.Id);
        Assert.AreEqual(dogToAdd.Name, result.Name);
        Assert.AreEqual(dogToAdd.Breed, result.Breed);
        Assert.AreEqual(dogToAdd.Weight, result.Weight);
    }

    private ApiMainContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        return new ApiMainContext(options);
    }
}
