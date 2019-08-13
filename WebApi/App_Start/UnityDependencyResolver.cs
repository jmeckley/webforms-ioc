using System.Web.Http.Dependencies;
using Unity;

namespace WebApi
{
    public class UnityDependencyResolver 
        : UnityDependencyScope
            , IDependencyResolver
    {
        public UnityDependencyResolver(IUnityContainer container) 
            : base(container)
        {
        }

        public IDependencyScope BeginScope() => new UnityDependencyScope(Container.CreateChildContainer());
    }
}