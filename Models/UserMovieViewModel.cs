using Mvc.Repositories.Data.Entities;


namespace Mvc.Models
{
    public class UserMovieViewModel
    {
        public IEnumerable<Users> GetUsers { get; set;}

        public UserMovieViewModel()
        {
            GetUsers = new List<Users>();
        }
    }
}