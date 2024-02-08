using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;
using NUnit.Framework;

[TestFixture]
public class UpdateBirdCommandHandlerTests
{
    [Test]
    public async Task UpdateBirdCommandHandler_ShouldUpdateBird()
    {
        // Arrange
        var birdRepositoryMock = new Mock<IBirdRepository>();
        var handler = new UpdateBirdCommandHandler(birdRepositoryMock.Object);

        var birdId = Guid.NewGuid();
        var existingBird = new Bird { Id = birdId, Name = "OldBird", CanFly = true, Color = "Blue" };
        birdRepositoryMock.Setup(repo => repo.GetBirdById(birdId)).ReturnsAsync(existingBird);

        var updatedBirdDto = new BirdDto { CanFly = false, Color = "Red" };
        var command = new UpdateBirdCommand(birdId, updatedBirdDto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(birdId, result.Id);
        Assert.AreEqual(existingBird.Name, result.Name);
        Assert.AreEqual(updatedBirdDto.CanFly, result.CanFly);
        Assert.AreEqual(updatedBirdDto.Color, result.Color);

        // Verify that the repository method was called
        birdRepositoryMock.Verify(repo => repo.UpdateBird(existingBird), Times.Once);
    }
}

