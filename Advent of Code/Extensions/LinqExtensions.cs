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
    }
}
