// @mteinum
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EfExt
{
    public static class Ext
    {
        private static readonly MethodInfo CompareTo = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
        private static readonly Expression Zero = Expression.Constant(0);

        private static IQueryable<TSource> StringCompare<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value,
            Func<Expression, Expression, Expression> method)
        {
            var exp = method(Expression.Call(
                keySelector.Body, CompareTo, Expression.Constant(value)), Zero);

            var lambda = Expression.Lambda<Func<TSource, bool>>(
                exp, keySelector.Parameters[0]);

            return source.Where(lambda);
        }

        public static IQueryable<TSource> GreaterThan<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
        {
            return source.StringCompare(
                keySelector,
                value,
                Expression.GreaterThanOrEqual);
        }

        public static IQueryable<TSource> LessThan<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
        {
            return source.StringCompare(
                keySelector,
                value,
                Expression.LessThanOrEqual);
        }

        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string lower, string upper)
        {
            return source.GreaterThan(keySelector, lower).LessThan(keySelector, upper);
        }

        // source.Where(lower.CompareTo(value) <= 0 && upper.CompareTo(value) >= 0);

        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> lowerKeySelector,
            Expression<Func<TSource, string>> upperKeySelector,
            string value)
        {
            return source.LessThan(lowerKeySelector, value)
                         .GreaterThan(upperKeySelector, value);
        }
    }
}