using System;
using System.Collections.Generic;

namespace WebApplication.Core
{
    public static class CollectionExtensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach(var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, T item)
        {
            foreach (var i in items)
            {
                yield return i;
            }

            yield return item;
        }
    }
}