using Mvc.Repositories.Data.Entities;

namespace Mvc.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movies>> GetMoviesAsync();
        Task<bool> UpdateMovieAsync(Movies movie);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> AddMovieAsync(Movies movie);
        Task<IEnumerable<Movies>> GetMovieByIdAsync(int id);
    }
}