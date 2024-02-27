using SuperHero.API.Data.Interfaces;
using Dapper;
using System.Data;

namespace SuperHero.API.Data.Dapper
{
    public class DapperConnection(IDbConnectionFactory dbConnectionFactory) : IConnection
    {
        public readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task ExecuteAsync(string proc, object param, CommandType commandType)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(proc, param);
        }

        public async Task<T?> ExecuteScalarAsync<T>(string sql, object param)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.ExecuteScalarAsync<T>(sql, param);
            return result;
        }

        public async Task<IEnumerable<T>?> QueryAsync<T>(string sql)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryAsync<T>(sql);
            return result;
        }

        public async Task<IEnumerable<T>?> QueryAsync<T>(string sql, object param)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryAsync<T>(sql, param);
            return result;
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object param)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            return result;
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<T>(sql);
            return result;
        }
    }
}
