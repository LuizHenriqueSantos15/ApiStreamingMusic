namespace ApiStreamingMusic.Application.Persistence
{
    using System.Data;
    using Microsoft.Extensions.Configuration;
    using Npgsql;

    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SupabaseDB");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
