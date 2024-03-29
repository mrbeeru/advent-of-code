﻿using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/3
    /// </summary>
    [Aoc(year: 2022, day: 3)]
    public class Day03(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
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

            return input.Chunk(3)
                .Select(x => CommonItem(x))
                .Select(commonItem => Priority(commonItem))
                .Sum();
        }

        private string[] SplitInHalf(string bag)
        {
            return new string[] { bag[..(bag.Length/2)], bag[(bag.Length/2)..] };
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
