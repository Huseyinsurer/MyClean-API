using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class UpdateCatByIdCommandHandlerTests
{
    [Test]
    public async Task UpdateCatByIdCommandHandler_ShouldUpdateCat()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var handler = new UpdateCatByIdCommandHandler(dbContext);

        var catId = Guid.NewGuid();
        var catToUpdate = new Cat { Id = catId, Name = "OldCat", LikesToPlay = true, Breed = "Siamese", Weight = 5 };
        dbContext.Cats.Add(catToUpdate);
        await dbContext.SaveChangesAsync();

        var updatedCatDto = new CatDto { Name = "NewCat", LikesToPlay = false, Breed = "Persian", Weight = 7 };
        var command = new UpdateCatByIdCommand(updatedCatDto, catId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(updatedCatDto.Name, result.Name);
        Assert.AreEqual(updatedCatDto.LikesToPlay, result.LikesToPlay);
        Assert.AreEqual(updatedCatDto.Breed, result.Breed);
        Assert.AreEqual(updatedCatDto.Weight, result.Weight);
    }

    private ApiMainContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        return new ApiMainContext(options);
    }
}
