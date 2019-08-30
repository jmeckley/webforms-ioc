using System.Data;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi
{
    public class UnitOfWorkAttribute
        : ActionFilterAttribute
    {
        private readonly IsolationLevel _isolationLevel;

        public UnitOfWorkAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _isolationLevel = isolationLevel;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var uow = GetUoW(actionContext.Request);
            return uow.Start(_isolationLevel, cancellationToken);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var uow = GetUoW(actionExecutedContext.Request);

            return actionExecutedContext.Exception == null ? uow.Commit(cancellationToken) : uow.Rollback(cancellationToken);
        }

        private IUnitOfWork GetUoW(HttpRequestMessage request)
        {
            return (IUnitOfWork)request.GetDependencyScope().GetService(typeof(IUnitOfWork));
        }
    }
}