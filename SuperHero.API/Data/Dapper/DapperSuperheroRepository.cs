using Dapper;
using Newtonsoft.Json;
using SuperHero.API.Data.Interfaces;
using SuperHero.API.Models;
using System.Data;

namespace SuperHero.API.Data.Dapper
{
    public class DapperSuperheroRepository(IConnection connection) : ISuperheroRepository
    {
        private readonly IConnection _connection = connection;

        public async Task<IEnumerable<Superhero>> GetAllAsync()
        {
            var superheroes = await _connection.QueryAsync<Superhero>("SELECT * FROM Superhero");
            return superheroes ?? [];
        }

        public async Task<Superhero> GetByIdAsync(int id)
        {
            var result = await _connection.QueryFirstOrDefaultAsync<Superhero?>("SELECT * FROM Superhero WHERE Id = @Id", new { Id = id });

            return result ?? throw new Exception($"Superhero with ID {id} not found");
        }

        public async Task<int> InsertAsync(Superhero superhero)
        {
            string jsonInput = JsonConvert.SerializeObject(superhero);

            var parameters = new DynamicParameters();
            parameters.Add("@SuperHero", jsonInput);
            parameters.Add("@SuperHeroId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("[dbo].[InsertSuperHero]", parameters, commandType: CommandType.StoredProcedure);

            int outputValue = parameters.Get<int>("@SuperHeroId");

            return outputValue;

        }

        public async Task<bool> UpdateAsync(Superhero superhero)
        {
            await _connection.ExecuteAsync("UPDATE Superhero SET Name = @Name, Powerstats = @Powerstats WHERE Id = @Id", superhero, CommandType.Text);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM Superhero WHERE Id = @Id", new { Id = id }, CommandType.Text);
            return true;
        }


        public async Task<IEnumerable<Superhero>> GetAllSuperherosAsync()
        {
            var result = await _connection.QueryFirstOrDefaultAsync<List<Superhero>>("SELECT * FROM Superhero");

            return result ?? [];
        }

        public async Task<IEnumerable<Superhero>> SearchSuperHeroAsync(string? search)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Search", search);

            var result = await _connection.QueryAsync<Superhero>("SELECT * FROM fnSearchSuperhero(@Search)", parameters);

            return result ?? [];
             
        }


    }
}
