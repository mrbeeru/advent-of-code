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
        /// Splits the source list into multiple lists each having count elements from the source list.
        /// </summary>
        /// <typeparam name="T">The type param.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="count">The sublist number of elements.</param>
        /// <returns>An IEnumerable containing multiple lists each having count elements from the source list.</returns>
        public static IEnumerable<IList<T>> GroupCount<T>(this IEnumerable<T> source, int count)
        {
            var output = new List<List<T>>();
            var temp = new List<T>();

            foreach (var element in source)
            {
                if (temp.Count == count)
                {
                    output.Add(temp);
                    temp = new List<T>();
                }

                temp.Add(element);
            }

            if (temp.Count > 0)
                output.Add(temp);

            return output;
        }

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
