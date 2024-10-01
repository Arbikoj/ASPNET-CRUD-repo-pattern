using Mvc.Repositories.Data.Entities;

namespace Mvc.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUserAsync();

        Task<IEnumerable<Users>> GetDataku();
    }
}