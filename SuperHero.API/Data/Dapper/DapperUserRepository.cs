using Dapper;
using SuperHero.API.Data.Interfaces;
using SuperHero.API.Models;
using SuperHero.API.Utils;
using System.Data;

namespace SuperHero.API.Data.Dapper
{
    public class DapperUserRepository(IConnection connection, IAuthUtils authUtils) : IUserRepository
    {
        private readonly IConnection _connection = connection;
        private readonly IAuthUtils _authUtils = authUtils;

        public async Task<User> CreateUserAsync(User newUser)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", newUser.Username);
            var passwordHashBytes = _authUtils.HashPassword(newUser.Username);
            parameters.Add("@PasswordHash", passwordHashBytes, DbType.Binary);
            var query = "INSERT INTO [User] (Username, PasswordHash) VALUES (@Username, @PasswordHash); SELECT SCOPE_IDENTITY();";
            var userId = await _connection.ExecuteScalarAsync<int>(query, parameters);
            newUser.Id = userId;
            return newUser;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var query = "SELECT * FROM [User] WHERE Username = @Username";
            var user = await _connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
            return user;
        }

        public async Task<User?> GetUserByUsernameAndHashAsync(string username, byte[] passwordHash)
        {
            var query = "SELECT * FROM [User] WHERE Username = @Username AND PasswordHash = @PasswordHash";
            var user = await _connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username, PasswordHash = passwordHash });
            return user;
        }
    }
}
