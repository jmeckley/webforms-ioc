using System;
using System.Linq;
using System.Reflection;
using Unity;
using WebApplication.Core;

namespace WebApplication
{
    public static class UnityRegistration
    {
        public static IUnityContainer RegisterTypesFromAssemblyContaining<TService>(this IUnityContainer container) => container.RegisterTypesFromAssemblyContaining<TService, TService>();
        
        public static IUnityContainer RegisterTypesFromAssemblyContaining<TService, TLocation>(this IUnityContainer container) => container.RegisterTypes<TService>(typeof(TLocation).Assembly);
        
        public static IUnityContainer RegisterTypes<TService>(this IUnityContainer container, params Assembly[] assemblies) => container.RegisterTypes(typeof(TService), assemblies);

        public static IUnityContainer RegisterTypes(this IUnityContainer container, Type @interface, params Assembly[] assemblies)
        {
            assemblies
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.IsAbstract == false)
                .Where(@interface.IsAssignableFrom)
                .Each(type => container.RegisterType(@interface, type));

            return container;
        }

        public static IUnityContainer RegisterDefaultTypesFromAssemblyContaining<T>(this IUnityContainer container) => container.RegisterDefaultTypesFromAssemblyContaining(typeof(T).Assembly);

        public static IUnityContainer RegisterDefaultTypesFromAssemblyContaining(this IUnityContainer container, params Assembly[] assemblies)
        {
            assemblies
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Select(type => new {type, @interface = type.GetInterface($"I{type.Name}")})
                .Where(_ => _.@interface != null)
                .Each(_ => container.RegisterType(_.@interface, _.type));

            return container;
        }
    }
}