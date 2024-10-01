using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Protrax.CostTracking.Repositories.Data
{
    public class FmlxMachDatabase : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection? _connection;
        private IDbTransaction? _transaction;

        public FmlxMachDatabase(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("FMLX_MACH_CONNECTION_STRING")
                                ?? throw new InvalidOperationException("FMLX-MACH connection string is missing in environment variables.");
        }

        public SqlConnection CreateConnection()
        {
            _connection = new SqlConnection(_connectionString);
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