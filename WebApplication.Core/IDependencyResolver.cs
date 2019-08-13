using System;
using System.Collections.Generic;

namespace WebApplication.Core
{
    public interface IDependencyResolver
    {
        object Resolve(Type serviceType);
        IEnumerable<object> ResolveAll(Type serviceType);
    }
}