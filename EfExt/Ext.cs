// @mteinum
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EfExt
{
    public static class Ext
    {
        /// <summary>
        /// 
        /// </summary>
        private static IQueryable<TSource> StringCompare<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value,
            Func<Expression, Expression, Expression> method)
        {
            var compareTo = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
            var zero = Expression.Constant(0);

            var exp = method(Expression.Call(
                keySelector.Body, compareTo, Expression.Constant(value)), zero);

            var lambda = Expression.Lambda<Func<TSource, bool>>(
                exp, keySelector.Parameters[0]);

            return source.Where(lambda);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> GreaterThanOrEqual<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
        {
            return source.StringCompare(
                keySelector,
                value,
                Expression.GreaterThanOrEqual);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> GreaterThan<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
        {
            return source.StringCompare(
                keySelector,
                value,
                Expression.GreaterThan);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> LessThanOrEqual<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
        {
            return source.StringCompare(
                keySelector,
                value,
                Expression.LessThanOrEqual);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> LessThan<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
        {
            return source.StringCompare(
                keySelector,
                value,
                Expression.LessThan);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string lower, string upper)
        {
            return source.GreaterThanOrEqual(keySelector, lower)
                         .LessThanOrEqual(keySelector, upper);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            string lower,
            Expression<Func<TSource, string>> keySelector,
            string upper)
        {
            return source.Between(keySelector, lower, upper);
        }

        // source.Where(lower.CompareTo(value) <= 0 && upper.CompareTo(value) >= 0);

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> lowerKeySelector,
            Expression<Func<TSource, string>> upperKeySelector,
            string value)
        {
            return source.LessThanOrEqual(lowerKeySelector, value)
                         .GreaterThanOrEqual(upperKeySelector, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<TSource> Between<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> lowerKeySelector,
            string value,
            Expression<Func<TSource, string>> upperKeySelector)
        {
            return source.Between(lowerKeySelector, upperKeySelector, value);
        }
    }
}