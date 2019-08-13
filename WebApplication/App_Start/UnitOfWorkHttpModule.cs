using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using WebApplication;

[assembly: PreApplicationStartMethod(typeof(UnitOfWorkHttpModule), nameof(UnitOfWorkHttpModule.Start))]
namespace WebApplication
{
    public class UnitOfWorkHttpModule
        : IHttpModule
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(UnitOfWorkHttpModule));
        }

        public void Init(HttpApplication context) => context.EndRequest += (sender, args) => UnityDependencyResolver.DisposeOfChildContainer();
        
        public void Dispose() {}
    }
}