
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IBirdRepository
    {
        Task<List<Bird>> GetAllBirds();
        Task<Bird?> GetBirdById(Guid birdId);
        Task AddBird(Bird bird, Guid userId);
        Task UpdateBird(Bird bird);
        Task DeleteBird(Bird bird);
    }
}
