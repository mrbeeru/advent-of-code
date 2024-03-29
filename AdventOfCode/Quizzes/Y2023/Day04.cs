﻿using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 4)]
    public class Day04(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            return inputProvider.GetInput()
                .Select(line => line.Nums())
                .Select(nums => (nums.Skip(1).Take(10), nums.Skip(11)))
                .Select(pair => pair.Item1.Intersect(pair.Item2).Count())
                .Select(val => val == 0 ? 0 : 1 << (val - 1))
                .Sum();
        }

        public long Part2()
        {
            return inputProvider.GetInput().Reverse()
                .Select(line => line.Nums())
                .Select(nums => (nums.Skip(1).Take(10), nums.Skip(11)))
                .Select((pair, idx) => (cnt: pair.Item1.Intersect(pair.Item2).Count(), idx))
                .Aggregate(new List<int>(), (l, pair) => l.Append(l.Skip(pair.idx - pair.cnt).Take(pair.cnt).Sum() + 1).ToList())
                .Sum();
        }
    }
}
