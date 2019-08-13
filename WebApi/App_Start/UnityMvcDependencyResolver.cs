using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace WebApi
{
    /*
     * This code is taken directory from Unity.Mvc5 repository.
     * The reason being there are only 3 small files in the nuget package.
     * Better to write this code ourselves and have full control over the Unity dependency, then reference another nuget package that provides little to no value.
     */
    public class UnityMvcDependencyResolver 
        : IDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private readonly IUnityContainer _container;

        public UnityMvcDependencyResolver(IUnityContainer container) => _container = container;

        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType)) return ChildContainer.Resolve(serviceType);
            return IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
            {
                yield return ChildContainer.Resolve(serviceType);
            }

            foreach (var service in ChildContainer.ResolveAll(serviceType))
            {
                yield return service;
            }
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

        private bool IsRegistered(Type typeToCheck)
        {
            var isRegistered = true;

            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                isRegistered = ChildContainer.IsRegistered(typeToCheck);

                if (!isRegistered && typeToCheck.IsGenericType)
                {
                    var openGenericType = typeToCheck.GetGenericTypeDefinition();

                    isRegistered = ChildContainer.IsRegistered(openGenericType);
                }
            }

            return isRegistered;
        }

        public static void DisposeOfChildContainer()
        {
            if (HttpContext.Current.Items[HttpContextKey] is IUnityContainer childContainer) childContainer.Dispose();
        }
    }
}