using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Repositories.Dogs
{
    public interface IDogRepository
    {
        Task<List<Dog>> GetAllDogs();
        Task<Dog?> GetDogById(Guid dogId);
        Task AddDog(Dog dog);
        Task UpdateDog(Dog dog);
        Task DeleteDog(Dog dog);
    }
}
