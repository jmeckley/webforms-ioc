using System;
using System.Collections.Concurrent;
using System.IO;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Unity;
using WebApi.Areas.HelpPage;
using WebApi.Areas.HelpPage.ModelDescriptions;
using WebApi.Areas.HelpPage.Models;
using WebApi.Controllers;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer()
                    .EnableDiagnostic()
                    
                    /*required to resolve controllers from the container instead of the default ctor*/
                    .RegisterType<IHttpControllerActivator, UnityHttpControllerActivator>()

                    /*example of registration required for IRepository*/
                    .RegisterType<IRepository, InMemoryRepository>()
                    .RegisterInstance(new ConcurrentDictionary<int, Entity>())

                    /*registered for convenience*/
                    .RegisterInstance(config)

                    /*dependencies of HelpController*/
                    .RegisterInstance(new XmlDocumentationProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "WebApi.xml")))
                    .RegisterType<IApiExplorer, ApiExplorer>()
                    .RegisterFactory<IDocumentationProvider>(_ => _.Resolve<XmlDocumentationProvider>())
                    .RegisterFactory<IModelDocumentationProvider>(_ => _.Resolve<XmlDocumentationProvider>())
                    .RegisterFactory<Func<string, HelpPageApiModel>>(_ => new Func<string, HelpPageApiModel>(config.GetHelpPageApiModel))
                ;

            /*WebApi resolver*/
            config.DependencyResolver = new UnityDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ValidationFilterAttribute());

            /*MVC resolver*/
            DependencyResolver.SetResolver(new UnityMvcDependencyResolver(container));
        }
    }
}
