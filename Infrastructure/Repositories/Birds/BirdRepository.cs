using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Birds
{
    public class BirdRepository : IBirdRepository
    {
        private readonly ApiMainContext _dbContext;
        private readonly ILogger<BirdRepository> _logger;

        public BirdRepository(ApiMainContext apiMainContext, ILogger<BirdRepository> logger)
        {
            _dbContext = apiMainContext;
            _logger = logger;
        }

        public async Task<List<Bird>> GetAllBirds()
        {
            try
            {
                List<Bird> allBirdsFromDatabase = await _dbContext.Birds.ToListAsync();
                return allBirdsFromDatabase;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all birds from the database", ex);
                throw new Exception("An error occurred while getting all birds from the database", ex);
            }
        }

        public async Task<Bird?> GetBirdById(Guid birdId)
        {
            try
            {
                Bird? wantedBird = await _dbContext.Birds.FindAsync(birdId);
                return wantedBird;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting a bird by Id {birdId} from the database", ex);
                throw new Exception($"An error occurred while getting a bird by Id {birdId} from the database", ex);
            }
        }

        public async Task AddBird(Bird bird, Guid userId)
        {
            try
            {
                // Lägg till fågel i databasen
                _dbContext.Birds.Add(bird);

                // Skapa ägarskapsrelation
                var ownership = new Ownership
                {
                    UserId = userId,
                    AnimalId = bird.Id
                };

                // Lägg till ägarskap i databasen
                _dbContext.Ownerships.Add(ownership);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a bird to the database", ex);
                throw new Exception($"An error occurred while adding a bird to the database", ex);
            }
        }

        public async Task UpdateBird(Bird bird)
        {
            try
            {
                // Uppdatera fågel i databasen
                _dbContext.Birds.Update(bird);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating a bird in the database", ex);
                throw new Exception($"An error occurred while updating a bird in the database", ex);
            }
        }

        public async Task DeleteBird(Bird bird)
        {
            try
            {
                // Ta bort fågel från databaskontexten
                _dbContext.Birds.Remove(bird);

                // Spara ändringar i databasen
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting a bird from the database", ex);
                throw new Exception($"An error occurred while deleting a bird from the database", ex);
            }
        }
    }
}
