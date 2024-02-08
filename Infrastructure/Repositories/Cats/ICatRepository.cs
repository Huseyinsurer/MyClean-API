// ICatRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Repositories.Cats
{
    public interface ICatRepository
    {
        Task<List<Cat>> GetAllCats();
        Task<Cat?> GetCatById(Guid catId);
        Task AddCat(Cat cat);
        Task UpdateCat(Cat cat);
        Task DeleteCat(Cat cat);
    }
}

