using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Y2022
{
    internal class Day03 : IAocDay<long>
    {
        private readonly IInputProvider inputProvider;

        public Day03(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();

            return input.Select(bag => SplitInHalf(bag))
                .Select(parts => CommonItem(parts))
                .Select(commonItem => Priority(commonItem))
                .Sum();
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();

            return input.GroupCount(3)
                .Select(x => CommonItem(x))
                .Select(commonItem => Priority(commonItem))
                .Sum();
        }

        private string[] SplitInHalf(string bag)
        {
            return new string[] {bag[..(bag.Length/2)], bag[(bag.Length/2)..]};
        }

        private char CommonItem(IEnumerable<string> parts)
        {
            return parts.Aggregate(
                    parts.First().AsEnumerable(), 
                    (intersectionResult, next) => intersectionResult.Intersect(next))
                .Single();
        }

        private int Priority(char c)
        {
            return c >= 'a' ?
                   c - 'a' + 1 :
                   c - 'A' + 27;
        }
    }
}
