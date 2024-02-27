using Microsoft.Extensions.Options;
using SuperHero.API.Configuration;
using SuperHero.API.Data.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace SuperHero.API.Data
{
    public class DbConnectionFactory(IOptions<AppSettings> appSettings) : IDbConnectionFactory
    {
        private readonly AppSettings _appSettings = appSettings.Value;
        public IDbConnection CreateConnection()
        {
            var db = new SqlConnection(_appSettings.ConnectionString)
                ?? throw new Exception("Unable to connect to database");
            return db;
        }
    }
}
