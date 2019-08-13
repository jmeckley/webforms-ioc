using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Unity;
using WebApplication.Core;

namespace WebApplication
{
    public static class UnitySettingsConvention
    {
        public static IUnityContainer RegisterSettingsFromAssemblyContaining<T>(this IUnityContainer container) => container.RegisterSettings(typeof(T).Assembly);
        public static IUnityContainer RegisterSettings(this IUnityContainer container, params Assembly[] assemblies)
        {
            assemblies
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.IsAbstract == false)
                .Where(type => type.GetConstructor(Type.EmptyTypes) != null)
                .Where(type => type.Name.EndsWith("Settings"))
                .Each(type =>
                {
                    var instance = Activator.CreateInstance(type);

                    type
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                        .Select(property => new { Property = property, Value = ConfigurationManager.AppSettings[$"{type.Name}.{property.Name}"] })
                        .Where(x => string.IsNullOrEmpty(x.Value) == false)
                        .Select(x => new { x.Property, Value = Convert.ChangeType(x.Value, x.Property.PropertyType) })
                        .Each(x => x.Property.SetValue(instance, x.Value, null));

                    container.RegisterInstance(type, instance);
                });
            
            return container;
        }
    }
}