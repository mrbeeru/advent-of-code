using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Extracts all the <see cref="int"/> values from a string.
        /// </summary>
        /// <param name="str">The input.</param>
        /// <returns>An IEnumerable containing all the <see cref="int"/> values found in input string.</returns>
        public static IEnumerable<int> Nums(this string str)
        {
            return Regex.Matches(str, @"-?\d+").Select(x => int.Parse(x.Value));
        }
    }
}
