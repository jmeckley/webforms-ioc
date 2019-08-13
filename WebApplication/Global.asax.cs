using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using WebApplication.Core;
using WebApplication.Core.Implementation;
using WebApplication.Core.Mvp.Default;

namespace WebApplication
{
    public class Global 
        : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var _container = new UnityContainer()
                    .RegisterType<IService, Service>()
                    .RegisterType<IRepository<DefaultViewModel>, DefaultViewModelRepository>()
                    .RegisterType<IValidator, DataAnnotationValidator>()
                    .RegisterType<IServiceProvider, UnityServiceProvider>()
                    .RegisterSettingsFromAssemblyContaining<Input>()
                ;

            DependencyResolver.Set(new UnityDependencyResolver(_container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}