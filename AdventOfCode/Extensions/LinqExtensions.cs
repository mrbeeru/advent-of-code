using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Extensions
{
    internal static class LinqExtensions
    {
        /// <summary>
        /// Computes the product of a sequence of <see cref="long"/> elements obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type param.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The product of the projected values.</returns>
        public static long Product<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return source.Aggregate(1L, (prod, next) => prod * selector(next));
        }

        /// <summary>
        /// Computes the product of a sequence of <see cref="int"/> elements obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type param.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The product of the projected values.</returns>
        public static long Product<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Aggregate(1L, (prod, next) => prod * selector(next));
        }
    }
}
