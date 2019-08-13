using System;
using System.Collections.Generic;
using System.Web;
using Unity;
using WebApplication.Core;

namespace WebApplication
{
    public class UnityDependencyResolver
        : IDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container) => _container = container;

        public object Resolve(Type serviceType)
        {
            return ChildContainer.Resolve(serviceType);
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            return ChildContainer.ResolveAll(serviceType);
        }

        protected IUnityContainer ChildContainer
        {
            get
            {
                var httpContext = HttpContext.Current;
                var items = httpContext.Items;

                if (items[HttpContextKey] is IUnityContainer childContainer) return childContainer;

                items[HttpContextKey] = childContainer = _container.CreateChildContainer();
                return childContainer;
            }
        }

        public static void DisposeOfChildContainer()
        {
            if (HttpContext.Current.Items[HttpContextKey] is IUnityContainer childContainer) childContainer.Dispose();
        }
    }
}