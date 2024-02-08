using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Moq;
using NUnit.Framework;

[TestFixture]
public class AddCatCommandHandlerTests
{
    [Test]
    public async Task AddCatCommandHandler_ShouldAddCat()
    {
        // Arrange
        var catRepositoryMock = new Mock<ICatRepository>();
        var handler = new AddCatCommandHandler(catRepositoryMock.Object);

        var catDto = new CatDto
        {
            Name = "TestCat",
            LikesToPlay = true,
            Breed = "Persian",
            Weight = 5
            // Andra egenskaper...
        };

        var command = new AddCatCommand(catDto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(catDto.Name, result.Name);
        Assert.AreEqual(catDto.LikesToPlay, result.LikesToPlay);
        Assert.AreEqual(catDto.Breed, result.Breed);
        Assert.AreEqual(catDto.Weight, result.Weight);

        // Verify that the repository method was called
        catRepositoryMock.Verify(repo => repo.AddCat(It.IsAny<Cat>()), Times.Once);
    }
}
