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

                    .RegisterInstance(config)
                    .RegisterInstance(new XmlDocumentationProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "WebApi.xml")))
                    .RegisterInstance(new ConcurrentDictionary<int, Entity>())

                    .RegisterType<IHttpControllerActivator, UnityHttpControllerActivator>()
                    .RegisterType<IApiExplorer, ApiExplorer>()
                    .RegisterType<IRepository, InMemoryRepository>()

                    .RegisterFactory<IDocumentationProvider>(_ => _.Resolve<XmlDocumentationProvider>())
                    .RegisterFactory<IModelDocumentationProvider>(_ => _.Resolve<XmlDocumentationProvider>())
                    .RegisterFactory<Func<string, HelpPageApiModel>>(_ => new Func<string, HelpPageApiModel>(config.GetHelpPageApiModel))
                ;

            config.DependencyResolver = new UnityDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ValidationFilterAttribute());

            DependencyResolver.SetResolver(new UnityMvcDependencyResolver(container));
        }
    }
}
