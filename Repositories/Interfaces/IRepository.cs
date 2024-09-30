namespace Mvc.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
