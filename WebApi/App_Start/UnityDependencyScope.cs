using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace WebApi
{
    public class UnityDependencyScope 
        : IDependencyScope
    {
        protected IUnityContainer Container { get; }

        public UnityDependencyScope(IUnityContainer container) => Container = container;

        public void Dispose() => Container.Dispose();

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (Exception)
            {
                //log warning, could not resolve serviceType
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                //log warning, could not resolve serviceType
                return null;
            }
        }
    }
}