using System;
using System.Data;

namespace WebApplication.Core.Database
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

        public void Start(IsolationLevel isolationLevel)
        {
            _context.Transaction = _connection.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _context.Transaction.Commit();
        }

        public void Rollback()
        {
            _context.Transaction.Rollback();
        }

        public void Dispose()
        {
            _context.Transaction?.Dispose();
        }
    }
}