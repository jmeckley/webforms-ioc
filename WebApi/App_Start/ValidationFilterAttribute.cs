using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi
{
    public class ValidationFilterAttribute
        : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var modelState = context.ModelState;
            if (modelState.IsValid) return;

            context.Response = context.Request.CreateErrorResponse(HttpStatusCodeAdditions.UnprocessableEntity, modelState);
        }
    }
}