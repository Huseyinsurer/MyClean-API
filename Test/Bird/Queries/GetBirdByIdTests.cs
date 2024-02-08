using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Birds.GetBirdById;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Birds;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GetBirdByIdTests
{
    [Test]
    public async Task GetBirdByIdQueryHandler_ShouldReturnBirdById()
    {
        // Arrange
        var birdRepositoryMock = new Mock<IBirdRepository>();
        var handler = new GetBirdByIdQueryHandler(birdRepositoryMock.Object);

        var birdId = Guid.NewGuid();
        var query = new GetBirdByIdQuery(birdId);

        var expectedBird = new Bird { Id = birdId, Name = "TestBird", CanFly = true, Color = "Blue" };
        birdRepositoryMock.Setup(repo => repo.GetBirdById(birdId)).ReturnsAsync(expectedBird);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedBird.Id, result.Id);
        Assert.AreEqual(expectedBird.Name, result.Name);
        Assert.AreEqual(expectedBird.CanFly, result.CanFly);
        Assert.AreEqual(expectedBird.Color, result.Color);

        // Verify that the repository method was called
        birdRepositoryMock.Verify(repo => repo.GetBirdById(birdId), Times.Once);
    }
}
