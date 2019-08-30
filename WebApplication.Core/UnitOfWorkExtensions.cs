using System;
using System.Data;

namespace WebApplication.Core
{
    public static class UnitOfWorkExtensions
    {
        public static void Execute(this IUnitOfWork uow, Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            try
            {
                uow.Start(isolationLevel);
                action();
                uow.Commit();
            }
            catch 
            {
                uow.Rollback();
                throw;
            }
        }

        public static T Execute<T>(this IUnitOfWork uow, Func<T> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var result = default(T);

            void Action() => result = action();

            uow.Execute(Action, isolationLevel);

            return result;
        }
    }
}