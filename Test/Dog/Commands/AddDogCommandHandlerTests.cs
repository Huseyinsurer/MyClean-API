using Application.Commands.Dogs;
using Application.Commands.Dogs.AddDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Dogs;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Dogs.Commands
{
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        [Test]
        public async Task AddDogCommandHandler_ShouldAddDog()
        {
            // Arrange
            var dogRepositoryMock = new Mock<IDogRepository>();
            var handler = new AddDogCommandHandler(dogRepositoryMock.Object);

            var dogDto = new DogDto { Name = "TestDog", Breed = "Labrador", Weight = 25 };
            var command = new AddDogCommand(dogDto);

            var expectedDog = new Dog { Id = Guid.NewGuid(), Name = "TestDog", Breed = "Labrador", Weight = 25 };

            dogRepositoryMock.Setup(repo => repo.AddDog(It.IsAny<Dog>()))
                .Callback<Dog>(dog =>
                {
                    // Simulera att det nya hunden har tilldelats ett ID
                    dog.Id = expectedDog.Id;
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(expectedDog.Id));
            Assert.That(result.Name, Is.EqualTo(expectedDog.Name));
            Assert.That(result.Breed, Is.EqualTo(expectedDog.Breed));
            Assert.That(result.Weight, Is.EqualTo(expectedDog.Weight));

            // Verify that the repository method was called
            dogRepositoryMock.Verify(repo => repo.AddDog(It.IsAny<Dog>()), Times.Once);
        }
    }
}
