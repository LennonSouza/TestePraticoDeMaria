using Npgsql;
using System;

namespace App.Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;
        private bool _disposed;

        public NpgsqlConnection Connection => _connection;
        public NpgsqlTransaction Transaction => _transaction;

        public UnitOfWork(ConnectionFactory factory)
        {
            _connection = factory.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit() => _transaction.Commit();

        public void Rollback() => _transaction.Rollback();

        public void Dispose()
        {
            if (_disposed) return;
            _transaction?.Dispose();
            _connection?.Dispose();
            _disposed = true;
        }
    }
}