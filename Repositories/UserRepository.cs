using MySql.Data.MySqlClient; // MySQL connection
using System.Data.SqlClient;
using Mvc.Repositories.Data.Entities;
using Mvc.Services;
using Dapper;
using Mvc.Repositories.Interfaces;
using Mvc.Repositories.Data;

namespace Mvc.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UsersDatabase _usersDatabaseConnection;
        private readonly ILogger<UserRepository> _userRepositoryLogger;
        public UserRepository(UsersDatabase usersDatabaseConnection, ILogger<UserRepository> userRepositoryLogger)
        {
            _usersDatabaseConnection= usersDatabaseConnection;
            _userRepositoryLogger = userRepositoryLogger;
        }



        public async Task<IEnumerable<Users>> GetUserAsync()
        {
            _userRepositoryLogger.LogInformation("{ClassName} - GetUserAsync opening database connection", this.GetType().Name);

            if (_usersDatabaseConnection == null)
            {
                _userRepositoryLogger.LogError("{ClassName} - _usersDatabaseConnection is null", this.GetType().Name);
                throw new InvalidOperationException("Database connection is not initialized.");
            }
            using(var connection = _usersDatabaseConnection.CreateConnection())
            {
                _userRepositoryLogger.LogInformation("{ClassName} - GetUserAsync: Connection opened and query executing", this.GetType().Name);
                var movie = await connection.QueryAsync<Users>(
                    "SELECT * FROM users;"
                );

                _userRepositoryLogger.LogInformation("{ClassName} - GetUserAsync: Query execution completed", this.GetType().Name);
                return movie.ToList();
            }
        }

    }
}

