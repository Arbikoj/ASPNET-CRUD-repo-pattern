using MySql.Data.MySqlClient; // MySQL connection
using System.Data.SqlClient;
using Mvc.Repositories.Data.Entities;
using Mvc.Services;
using Dapper;
using Mvc.Repositories.Interfaces;
using Mvc.Repositories.Data;
using Protrax.CostTracking.Repositories.Data;

namespace Mvc.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UsersDatabase _usersDatabaseConnection;
        private readonly MoviesDatabase _movieDatabaseConnection;

        private readonly FmlxMachDatabase _fmlxMachDatabaseConnection;
        private readonly ILogger<UserRepository> _userRepositoryLogger;
        public UserRepository(UsersDatabase usersDatabaseConnection, MoviesDatabase moviesDatabaseConnection, FmlxMachDatabase fmlxMachDatabaseConnection, ILogger<UserRepository> userRepositoryLogger)
        {
            _usersDatabaseConnection= usersDatabaseConnection;
            _userRepositoryLogger = userRepositoryLogger;
            _movieDatabaseConnection = moviesDatabaseConnection;
            _fmlxMachDatabaseConnection = fmlxMachDatabaseConnection;
        }

        public async Task<IEnumerable<Users>> GetDataku()
        {
            _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync opening database connection", this.GetType().Name);

            if (_usersDatabaseConnection == null)
            {
                _userRepositoryLogger.LogError("{ClassName} - _usersDatabaseConnection is null", this.GetType().Name);
                throw new InvalidOperationException("Database connection is not initialized.");
            }
            using(var connection = _usersDatabaseConnection.CreateConnection())
            {
                _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Connection opened and query executing", this.GetType().Name);
                var movie = await connection.QueryAsync<Users>(
                    "SELECT * FROM users;"
                );

                _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Query execution completed", this.GetType().Name);
                return movie;
            }
        }

        public Task<IEnumerable<Users>> GetUserAsync()
        {
            _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync opening database connection", this.GetType().Name);

            if (_usersDatabaseConnection == null)
            {
                _userRepositoryLogger.LogError("{ClassName} - _usersDatabaseConnection is null", this.GetType().Name);
                throw new InvalidOperationException("Database connection is not initialized.");
            }
            using(var connection = _usersDatabaseConnection.CreateConnection())
            {
                _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Connection opened and query executing", this.GetType().Name);
                var movie =  connection.QueryAsync<Users>(
                    "SELECT * FROM users;"
                );

                _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Query execution completed", this.GetType().Name);
                return movie;
            }
        }



        // public async Task<IEnumerable<Users>> GetUserAsync()
        // {
        //     _userRepositoryLogger.LogInformation("{ClassName} - GetUserAsync opening database connection", this.GetType().Name);

        //     if (_usersDatabaseConnection == null)
        //     {
        //         _userRepositoryLogger.LogError("{ClassName} - _usersDatabaseConnection is null", this.GetType().Name);
        //         throw new InvalidOperationException("Database connection is not initialized.");
        //     }
        //     using(var connection = _usersDatabaseConnection.CreateConnection())
        //     {
        //         _userRepositoryLogger.LogInformation("{ClassName} - GetUserAsync: Connection opened and query executing", this.GetType().Name);
        //         var movie = await connection.QueryAsync<Users>(
        //             "SELECT * FROM users;"
        //         );

        //         _userRepositoryLogger.LogInformation("{ClassName} - GetUserAsync: Query execution completed", this.GetType().Name);
        //         return movie.ToList();
        //     }
        // }

        // public async Task<IEnumerable<Users>> GetUsersAsync()
        // {
        //     _userRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync opening database connection", this.GetType().Name);
        //     using(var connection = _moviesDatabaseConnection.CreateConnection())
        //     {
        //         _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Connection opened and query executing", this.GetType().Name);
        //         var movie = await connection.QueryAsync<Movies>(
        //             "SELECT * FROM movies;"
        //         );

        //         _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Query execution completed", this.GetType().Name);
        //         return movie.ToList();
        //     }
        // }

    }
}

