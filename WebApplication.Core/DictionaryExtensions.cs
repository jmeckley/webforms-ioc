using System.Collections;

namespace WebApplication.Core
{
    public static class DictionaryExtensions
    {
        public static void Put<T>(this IDictionary dictionary, string key, T item)
        {
            if (dictionary.Contains(key)) return;
            dictionary.Add(key, item);
        }

        public static T Get<T>(this IDictionary dictionary, string key)
        {
            return (T)dictionary[key];
        }

        public static void Put<T>(this IDictionary dictionary, T item)
        {
            var key = typeof(T).FullName;
            dictionary.Put(key, item);
        }

        public static T Get<T>(this IDictionary dictionary)
        {
            var key = typeof(T).FullName;
            return dictionary.Get<T>(key);
        }
    }
}