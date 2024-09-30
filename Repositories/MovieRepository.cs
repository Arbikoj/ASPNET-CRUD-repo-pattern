using MySql.Data.MySqlClient; // MySQL connection
using System.Data.SqlClient;
using Mvc.Repositories.Data.Entities;
using Mvc.Services;
using Dapper;
using Mvc.Repositories.Interfaces;
using Mvc.Repositories.Data;

namespace Mvc.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        // private readonly string _connectionString;
        // private readonly ILogService _movieRepositoryLogger;
        private readonly MoviesDatabase _moviesDatabaseConnection;
        private readonly ILogger<MovieRepository> _movieRepositoryLogger;

        // public MovieRepository(string connectionString, ILogService logService)
        // {
        //     _connectionString = connectionString;
        //     _movieRepositoryLogger = logService;
        // }

        public MovieRepository(MoviesDatabase moviesDatabaseConnection, ILogger<MovieRepository> movieRepositoryLogger)
        {
            _moviesDatabaseConnection= moviesDatabaseConnection;
            _movieRepositoryLogger = movieRepositoryLogger;
        }

        public async Task<IEnumerable<Movies>> GetMoviesAsync()
        {
            _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync opening database connection", this.GetType().Name);
            using(var connection = _moviesDatabaseConnection.CreateConnection())
            {
                _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Connection opened and query executing", this.GetType().Name);
                var movie = await connection.QueryAsync<Movies>(
                    "SELECT * FROM movies;"
                );

                _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesAsync: Query execution completed", this.GetType().Name);
                return movie.ToList();
            }
        }

        public async Task<bool> UpdateMovieAsync(Movies movie)
        {
            using (var connection = _moviesDatabaseConnection.CreateConnection())
            {
                string sql = @"
                        UPDATE 
                            movies
                        SET 
                            nama = @nama, 
                            genre = @genre 
                        WHERE 
                            Id = @id";
                var result = await connection.ExecuteAsync(sql, new 
                { 
                    movie.nama, 
                    movie.genre, 
                    movie.Id 
                });

                if(result > 0)
                {
                    _movieRepositoryLogger.LogInformation("UpdateMovieAsync: Movie with Idx {Id} updated successfully", movie.Id);
                }
                else
                {
                    _movieRepositoryLogger.LogWarning("UpdateAsync: No rows updated for Movie with Idx {Id}", movie.Id);

                }
                return result > 0;
            }
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            using (var connection = _moviesDatabaseConnection.CreateConnection())
            {
                string sql = "DELETE FROM Movies WHERE Id = @Id";
                var result = await connection.ExecuteAsync(sql, new { Id = id });

                if (result > 0)
                {
                    _movieRepositoryLogger.LogInformation("DeleteMovieAsync: Movie with Idx {id} deleted successfully", id);
                }
                else
                {
                    _movieRepositoryLogger.LogWarning("DeleteMovieAsync: No Movie found with Idx {id} to delete", id);
                }
                return result > 0;
            }
        }

        public async Task<bool> AddMovieAsync(Movies movie)
        {
            _movieRepositoryLogger.LogInformation("{ClassName} - AddMovieAsync opening database connection", this.GetType().Name);

            using (var connection = _moviesDatabaseConnection.CreateConnection())
            {
                _movieRepositoryLogger.LogInformation("{ClassName} - AddMovieAsync: Connection opened and query executing", this.GetType().Name);
                string sql = @"
                        INSERT INTO 
                            Movies (nama, genre) 
                        VALUES 
                            (@nama, @genre)";
                var result = await connection.ExecuteAsync(sql, new { movie.nama, movie.genre });
                
                _movieRepositoryLogger.LogInformation("AddMovieAsync: Movie created successfully");

                return result > 0;
            }
        }

        public async Task<IEnumerable<Movies>> GetMovieByIdAsync(int id)
        {
            _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesById opening database connection", this.GetType().Name);
            using(var connection = _moviesDatabaseConnection.CreateConnection())
            {
                _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesByIdAsync: Connection opened and query executing", this.GetType().Name);
                var movie = await connection.QueryAsync<Movies>(
                    "SELECT * FROM movies WHERE Id=@Id;",
                    new { Id = id }
                );

                _movieRepositoryLogger.LogInformation("{ClassName} - GetMoviesByIdAsync: Query execution completed", this.GetType().Name);
                return movie.ToList();
            }
        }
    }
}

