using Dapper;
using SuperHero.API.Data.Interfaces;

namespace SuperHero.API.Data.Dapper
{
    public class DapperFavoriteRepository(IConnection connection) : IFavoriteRepository
    {
    
        private readonly IConnection _connection = connection;
        public async Task AddAsync(int superHeroId, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SuperheroId", superHeroId);
            parameters.Add("@UserId", userId);

            var query = "INSERT INTO [Favorites] (SuperheroId, UserId) VALUES (@SuperheroId, @UserId);";
            await _connection.ExecuteScalarAsync<int>(query, parameters);
        }


        public async Task<List<int>> GetAsync(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            var query = "SELECT * FROM [Favorites] WHERE UserId = @UserId;";
            var results = await _connection.ExecuteScalarAsync<List<int>>(query, parameters);
            
            return results ?? [];

        }
    }
}
