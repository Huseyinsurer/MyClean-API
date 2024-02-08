using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Birds.UpdateBird;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Birds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Test.Birds.Commands
{
    [TestFixture]
    public class UpdateBirdCommandHandlerTests
    {
        private ApiMainContext _dbContext;
        private IBirdRepository _birdRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApiMainContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new ApiMainContext(options);
            var loggerMock = new Mock<ILogger<BirdRepository>>();
            _birdRepository = new BirdRepository(_dbContext, loggerMock.Object);
        }

        [Test]
        public async Task UpdateBirdCommandHandler_ShouldUpdateBird()
        {
            // ... (resten av testkoden som tidigare)
        }

        [Test]
        public async Task UpdateBirdCommandHandler_ShouldReturnNull_WhenBirdNotFound()
        {
            // ... (resten av testkoden som tidigare)
        }

        // Add more tests as needed

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}

