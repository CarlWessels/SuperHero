using System.Data;

namespace SuperHero.API.Data.Interfaces
{
    public interface IConnection
    {
        Task<T?> ExecuteScalarAsync<T>(string sql, object param);
        Task <T?> QueryFirstOrDefaultAsync<T>(string sql, object param);
        Task<T?> QueryFirstOrDefaultAsync<T>(string sql);
        Task<IEnumerable<T>?> QueryAsync<T>(string sql);
        Task<IEnumerable<T>?> QueryAsync<T>(string sql, object param);
        Task ExecuteAsync(string proc, object param, CommandType commandType);
    }
}

