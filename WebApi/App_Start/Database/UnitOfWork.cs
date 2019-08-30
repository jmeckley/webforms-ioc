using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public class UnitOfWork
        : IUnitOfWork
            , IDisposable
    {
        //for this example the methods are synchronous, but in other libraries these calls can be async, so the interface supports that.
        private readonly IDbConnection _connection;
        private readonly DatabaseConnectionContext _context;
        private IDbTransaction _transaction;

        public UnitOfWork(IDbConnection connection, DatabaseConnectionContext context)
        {
            _connection = connection;
            _context = context;
        }

        public Task Start(IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            _context.Transaction = _connection.BeginTransaction(isolationLevel);
            return Task.CompletedTask;
        }

        public Task Commit(CancellationToken cancellationToken)
        {
            _context.Transaction.Commit();
            return Task.CompletedTask;
        }

        public Task Rollback(CancellationToken cancellationToken)
        {
            _context.Transaction.Rollback();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _context.Transaction?.Dispose();
        }
    }
}