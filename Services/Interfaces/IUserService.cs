
using System.Collections.Generic;
using Mvc.Repositories.Data.Entities;

namespace Mvc.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetUserAsync();
        Task<IEnumerable<Users>> GetDataku();
    }
}