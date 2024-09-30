using Mvc.Repositories.Data.Entities;


namespace Mvc.Models
{
    public class MovieViewModel
    {
        public IEnumerable<Movies> GetMovies { get; set;}

        public MovieViewModel()
        {
            GetMovies = new List<Movies>();
        }
    }
}