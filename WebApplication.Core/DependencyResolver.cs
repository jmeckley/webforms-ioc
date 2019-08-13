using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Core
{
    public static class DependencyResolver
    {
        public static IDependencyResolver Current { get; private set; }

        public static void Set(IDependencyResolver resolver)
        {
            Current = resolver;
        }

        public static IEnumerable<T> ResolveAll<T>(this IDependencyResolver resolver) => resolver.ResolveAll(typeof(T)).Cast<T>();

        public static T Resolve<T>(this IDependencyResolver resolver) => (T) resolver.Resolve(typeof(T));
    }
}