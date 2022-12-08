using AdventOfCode.Reader;
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

        private IEnumerable<long> GetCaloriesPerElf()
        {
            var lines = inputProvider.GetInput();
            var calories = new List<long>();
            var current = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    calories.Add(current);
                    current = 0;
                    continue;
                }

                current += int.Parse(line);
            }

            return calories;
        }

    }
}
