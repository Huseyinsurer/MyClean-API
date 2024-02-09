using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database;

public class UserAnimalRepository : IUserAnimalRepository
{
    private readonly ApiMainContext _dbContext;

    public UserAnimalRepository(ApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AssociateUserWithAnimal(Ownership ownership)
    {
        try
        {
            _dbContext.Ownerships.Add(ownership);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DisassociateUserFromAnimal(Ownership ownership)
    {
        try
        {
            var existingOwnership = await _dbContext.Ownerships
                .FirstOrDefaultAsync(o => o.UserId == ownership.UserId && o.AnimalId == ownership.AnimalId);

            if (existingOwnership != null)
            {
                _dbContext.Ownerships.Remove(existingOwnership);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Ownership>> GetAllAssociatedAnimals()
    {
        return await _dbContext.Ownerships.ToListAsync();
    }
}
