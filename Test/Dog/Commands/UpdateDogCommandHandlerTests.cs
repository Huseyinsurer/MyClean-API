using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class UpdateDogByIdCommandHandlerTests
{
    [Test]
    public async Task UpdateDogByIdCommandHandler_ShouldUpdateDog()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var handler = new UpdateDogByIdCommandHandler(dbContext);

        var dogId = Guid.NewGuid();
        var dogToUpdate = new Dog { Id = dogId, Name = "OldDog", Breed = "Labrador", Weight = 25 };
        dbContext.Dogs.Add(dogToUpdate);
        await dbContext.SaveChangesAsync();

        var updatedDogDto = new DogDto { Name = "NewDog", Breed = "Poodle", Weight = 20 };
        var command = new UpdateDogByIdCommand(updatedDogDto, dogId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(updatedDogDto.Name, result.Name);
        Assert.AreEqual(updatedDogDto.Breed, result.Breed);
        Assert.AreEqual(updatedDogDto.Weight, result.Weight);
    }

    private ApiMainContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        return new ApiMainContext(options);
    }
}

