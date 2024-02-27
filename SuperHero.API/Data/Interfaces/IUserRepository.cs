
using SuperHero.API.Models;

namespace SuperHero.API.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User newUser);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByUsernameAndHashAsync(string username, byte[] passwordHash);
    }
}
