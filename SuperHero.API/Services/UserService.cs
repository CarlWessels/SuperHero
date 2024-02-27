using SuperHero.API.Data.Interfaces;
using SuperHero.API.Models;
using SuperHero.API.Services.Interfaces;
using SuperHero.API.Utils;

namespace SuperHero.API.Services
{
    public class UserService(IUserRepository userRepository, IAuthUtils authUtils) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthUtils _authUtils = authUtils;


        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var hashedInputPassword = _authUtils.HashPassword(password);
            var user = await _userRepository.GetUserByUsernameAndHashAsync(username, hashedInputPassword);
            return user 
                ?? throw new Exception("TODO THROW PROPER EXCEPTION -- failed auth");
        }

        public async Task<int?> RegisterAsync(string username, string password)
        {
            if (await _userRepository.GetUserByUsernameAsync(username) != null)
                throw new Exception("TODO THROW PROPER EXCEPTION -- user exists");

            var newUser = new User()
            {
                Username = username,
                Password = Convert.ToBase64String(_authUtils.HashPassword(password))
            };

            var createdUser = await _userRepository.CreateUserAsync(newUser);
            return createdUser.Id;
        }
    }
}