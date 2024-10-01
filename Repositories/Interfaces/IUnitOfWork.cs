using Mvc.Repositories.Interfaces;

namespace Mvc.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
    IMovieRepository MovieRepository { get; }   
    IUserRepository UserRepository { get; }   
    // Repository 3
    // ..
    // ..
    // Repository ..
    void BeginTransaction();
    Task<int> CommitAsync();
    Task RollbackAsync();
    void Dispose();
    
    }

}