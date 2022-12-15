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
        public static IEnumerable<int> Nums(this string str)
        {
            return Regex.Matches(str, @"-?\d+").Select(x => int.Parse(x.Value));
        }
    }
}
