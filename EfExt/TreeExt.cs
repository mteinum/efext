using System;
using System.Collections.Generic;
using System.Linq;

namespace EfExt
{
    public static class TreeExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Recursive<T>(
            this T node, Func<T, IEnumerable<T>> selector)
        {
            yield return node;
            var children = selector(node);
            if (children == null) yield break;

            foreach (var child in children.SelectMany(x => x.Recursive(selector)))
            {
                yield return child;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToRoot<T>(
            T item, Func<T, T> selector)
        {
            if (Equals(item, default(T))) yield break;
            yield return item;

            foreach (var x in ToRoot(selector(item), selector))
                yield return x;
        }

        /// <summary>
        /// Opposite of Any
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Empty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
    }
}
