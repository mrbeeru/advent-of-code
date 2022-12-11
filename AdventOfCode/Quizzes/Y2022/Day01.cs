using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/1
    /// </summary>
    internal class Day01 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day01(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            return GetCaloriesPerElf().Max();
        }

        public long Part2()
        {
            return GetCaloriesPerElf()
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();
        }

        private IEnumerable<int> GetCaloriesPerElf()
        {
            return inputProvider.GetInput()
                .Split(x => string.IsNullOrWhiteSpace(x)).Select(x => x.Select(y => int.Parse(y)).Sum());
        }

    }
}
