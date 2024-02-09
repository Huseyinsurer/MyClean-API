// UserRepository.cs
using System;
using System.Threading.Tasks;
using Domain.Models.User;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiMainContext _dbContext;

        public UserRepository(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUser(UserModel user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            // Implementera logiken för att uppdatera en användare här
        }

        public async Task DeleteUserAsync(UserModel user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
