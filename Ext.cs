// @mteinum
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EfExt
{
    public static class Ext
    {
        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string lower, string upper)
        {
            var zero = Expression.Constant(0);
            var compareTo = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });

            var loExp = Expression.GreaterThanOrEqual(
                Expression.Call(keySelector.Body, compareTo, Expression.Constant(lower)), zero);

            var hiExp = Expression.LessThanOrEqual(
                Expression.Call(keySelector.Body, compareTo, Expression.Constant(upper)), zero);

            var lambda = Expression.Lambda<Func<TSource, bool>>(
                Expression.AndAlso(loExp, hiExp), keySelector.Parameters[0]);

            return source.Where(lambda);
        }
    }
}