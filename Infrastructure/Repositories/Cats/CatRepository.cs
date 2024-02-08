// CatRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Cats
{
    public class CatRepository : ICatRepository
    {
        private readonly ApiMainContext _dbContext;
        private readonly ILogger<CatRepository> _logger;

        public CatRepository(ApiMainContext cleanApiMainContext, ILogger<CatRepository> logger)
        {
            _dbContext = cleanApiMainContext;
            _logger = logger;
        }

        public async Task<List<Cat>> GetAllCats()
        {
            try
            {
                List<Cat> allCatsFromDatabase = await _dbContext.Cats.ToListAsync();
                return allCatsFromDatabase;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all cats from the database", ex);
                throw new Exception("An error occurred while getting all cats from the database", ex);
            }
        }

        public async Task<Cat?> GetCatById(Guid catId)
        {
            try
            {
                Cat? wantedCat = await _dbContext.Cats.FindAsync(catId);
                return wantedCat;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting a cat by Id {catId} from the database", ex);
                throw new Exception($"An error occurred while getting a cat by Id {catId} from the database", ex);
            }
        }

        public async Task AddCat(Cat cat)
        {
            try
            {
                // Add cat to the database
                _dbContext.Cats.Add(cat);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a cat to the database", ex);
                throw new Exception($"An error occurred while adding a cat to the database", ex);
            }
        }

        public async Task UpdateCat(Cat cat)
        {
            try
            {
                // Update cat in the database
                _dbContext.Cats.Update(cat);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating a cat in the database", ex);
                throw new Exception($"An error occurred while updating a cat in the database", ex);
            }
        }

        public async Task DeleteCat(Cat cat)
        {
            try
            {
                // Remove cat from the database context
                _dbContext.Cats.Remove(cat);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting a cat from the database", ex);
                throw new Exception($"An error occurred while deleting a cat from the database", ex);
            }
        }
    }
}
