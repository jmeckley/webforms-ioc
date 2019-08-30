using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    /*
     * This should only be used for fetching and saving a single entity.
     * If you need to select a list, or projection, then build a separate object/command for that.
     * Otherwise the repository interface/implementation can become a god class with dozens or even hundreds of methods thus creating a monolithic class, which we want to avoid
     */
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id, CancellationToken cancellationToken);
        Task<T> Insert(T item, CancellationToken cancellationToken);
        Task<T> Update(T item, CancellationToken cancellationToken);
        Task Delete(T item, CancellationToken cancellationToken);
    }
}