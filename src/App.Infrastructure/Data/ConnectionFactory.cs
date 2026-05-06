using Npgsql;

namespace App.Infrastructure.Data
{
    public class ConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}