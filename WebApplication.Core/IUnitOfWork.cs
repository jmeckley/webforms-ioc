using System.Data;

namespace WebApplication.Core
{
    public interface IUnitOfWork
    {
        void Start(IsolationLevel isolationLevel);
        void Commit();
        void Rollback();
    }
}