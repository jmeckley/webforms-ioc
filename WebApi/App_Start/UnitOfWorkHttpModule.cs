using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using WebApi;

[assembly: PreApplicationStartMethod(typeof(UnitOfWorkHttpModule), nameof(UnitOfWorkHttpModule.Start))]
namespace WebApi
{
    public class UnitOfWorkHttpModule
        : IHttpModule
    {
        public static void Start() => DynamicModuleUtility.RegisterModule(typeof(UnitOfWorkHttpModule));

        public void Init(HttpApplication context) => context.EndRequest += (sender, args) => UnityMvcDependencyResolver.DisposeOfChildContainer();

        public void Dispose() { }
    }
}