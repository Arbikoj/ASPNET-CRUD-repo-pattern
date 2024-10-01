using System.Data;
using System.Data.SqlClient;
using Mvc.Repositories.Data;
using Mvc.Repositories.Interfaces;

using Dapper;
using Protrax.CostTracking.Repositories.Data;

namespace Mvc.Repositories 
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MoviesDatabase _moviesDatabaseConnection;
        private readonly UsersDatabase _usersDatabaseConnection;
        private readonly FmlxMachDatabase _fmlxMachDatabaseConnection;

        private IDbTransaction? _transaction;
        private IMovieRepository? _movieRepository;
        private IUserRepository? _userRepository;

        private readonly ILogger<MovieRepository> _movieRepositoryLogger;
        private readonly ILogger<UserRepository> _userRepositoryLogger;

        public UnitOfWork(
            // harus declare database juga
            MoviesDatabase moviesDatabase,
            UsersDatabase usersDatabase,
            FmlxMachDatabase fmlxMachDatabase,
            ILogger<MovieRepository> movieRepositoryLogger,
            ILogger<UserRepository> userRepositoryLogger
        )
        {
            _moviesDatabaseConnection = moviesDatabase;
            _usersDatabaseConnection = usersDatabase;
            _fmlxMachDatabaseConnection = fmlxMachDatabase;
            _movieRepositoryLogger = movieRepositoryLogger;
            _userRepositoryLogger = userRepositoryLogger;
        }

        public IMovieRepository MovieRepository => 
            _movieRepository ??= new MovieRepository(_moviesDatabaseConnection, _movieRepositoryLogger);

        public IUserRepository UserRepository => 
            _userRepository ??= new UserRepository(_usersDatabaseConnection, _moviesDatabaseConnection, _fmlxMachDatabaseConnection,  _userRepositoryLogger);

        public void BeginTransaction()
        {
            _transaction = _moviesDatabaseConnection.BeginTransaction();
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                _transaction?.Commit();
                return 1;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public void Dispose()
        {
            DisposeTransaction();
            _moviesDatabaseConnection?.Dispose();
        }

        public Task RollbackAsync()
        {
            _transaction?.Rollback();
            return Task.CompletedTask;
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
