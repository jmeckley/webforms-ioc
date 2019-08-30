using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Lifetime;
using WebApplication.Core;
using WebApplication.Core.Database;
using WebApplication.Core.Implementation;
using WebApplication.Core.Mvp.Default;
using WebApplication.Core.Mvp.MyEntity;

namespace WebApplication
{
    public class Global 
        : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var _container = new UnityContainer()
                    .RegisterType<IService, Service>()
                    .RegisterType<IProjection<DefaultViewModel>, DefaultViewModelRepository>()
                    .RegisterType<IValidator, DataAnnotationValidator>()
                    .RegisterType<IServiceProvider, UnityServiceProvider>()
                    /*convention to load settings from web.config*/
                    .RegisterSettingsFromAssemblyContaining<Input>()

                    /*database*/
                    .RegisterInstance(ConfigurationManager.ConnectionStrings["Default"])
                    .RegisterType<DatabaseConnectionFactory>(new SingletonLifetimeManager())
                    .RegisterFactory<IDbConnection>(_ => _.Resolve<DatabaseConnectionFactory>().Create(), new HierarchicalLifetimeManager())
                    .RegisterType<DatabaseConnectionContext>(new HierarchicalLifetimeManager())
                    .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
                    .RegisterType<ICrudRepository<Core.Model.MyEntity>, DapperMyEntityRepository>(new HierarchicalLifetimeManager())
                ;

            /*follows the same approach as MS MVC DependencyResolver*/
            DependencyResolver.Set(new UnityDependencyResolver(_container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}