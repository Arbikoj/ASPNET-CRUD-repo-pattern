using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Mvc.Repositories.Data
{
    public class MoviesDatabase : IDisposable
    {
        private readonly string _connectionString;
        private MySqlConnection? _connection;
        private IDbTransaction? _transaction;

        public MoviesDatabase(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("MOVIES_LOCAL_CONNECTION")
                            ?? throw new InvalidOperationException("MOVIES DATABASE connection string is missing in environment variables.");
        }

        public MySqlConnection CreateConnection()
        {
            _connection = new MySqlConnection(_connectionString);
            return _connection;
        }

        public void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = CreateConnection();
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public IDbTransaction BeginTransaction()
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("Connection must be opened before beginning a transaction.");
            }

            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
