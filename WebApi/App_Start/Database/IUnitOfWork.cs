using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public interface IUnitOfWork
    {
        Task Start(IsolationLevel isolationLevel, CancellationToken cancellationToken);
        Task Commit(CancellationToken cancellationToken);
        Task Rollback(CancellationToken cancellationToken);
    }
}