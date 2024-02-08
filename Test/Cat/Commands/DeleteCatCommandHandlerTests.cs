using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Cats.DeleteCat;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class DeleteCatCommandHandlerTests
{
    [Test]
    public async Task DeleteCatCommandHandler_ShouldDeleteCat()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var handler = new DeleteCatCommandHandler(dbContext);

        var catId = Guid.NewGuid();
        var catToRemove = new Cat { Id = catId, Name = "TestCat" };
        dbContext.Cats.Add(catToRemove);
        await dbContext.SaveChangesAsync();

        var command = new DeleteCatCommand(catId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNull(dbContext.Cats.FirstOrDefault(c => c.Id == catId));
    }

    private ApiMainContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApiMainContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        return new ApiMainContext(options);
    }
}
