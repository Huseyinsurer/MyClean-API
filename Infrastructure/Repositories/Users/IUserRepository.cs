using System;
using System.Threading.Tasks;
using Domain.Models.User;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByUsername(string username);
        Task<UserModel> GetUserById(Guid userId);
        Task AddUser(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(UserModel user);
        Task SaveChangesAsync();
        
    }
}
