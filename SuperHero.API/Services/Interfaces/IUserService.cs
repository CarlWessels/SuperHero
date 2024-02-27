using SuperHero.API.Models;

namespace SuperHero.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<int?> RegisterAsync(string username, string password);
    }
}
