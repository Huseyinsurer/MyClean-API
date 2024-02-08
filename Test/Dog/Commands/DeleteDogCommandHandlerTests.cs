using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class DeleteDogCommandHandlerTests
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
    public async Task DeleteDogCommandHandler_ShouldDeleteDog()
    {
        // Arrange
        var dogId = Guid.NewGuid();
        var handler = new DeleteDogCommandHandler(_dbContext);

        // Add a dog to the database for testing deletion
        var dogToAdd = new Dog { Id = dogId, Name = "TestDog", Breed = "Labrador", Weight = 25 };
        _dbContext.Dogs.Add(dogToAdd);
        await _dbContext.SaveChangesAsync();

        var command = new DeleteDogCommand(dogId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        Assert.IsNull(_dbContext.Dogs.FirstOrDefault(d => d.Id == dogId));
    }

    // Add more tests as needed

    [TearDown]
    public void TearDown()
    {
        // Clean up resources, close database connection, etc.
        _dbContext.Dispose();
    }
}
