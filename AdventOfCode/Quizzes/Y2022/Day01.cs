using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/1
    /// </summary>
    [Aoc(year: 2022, day: 1)]
    public class Day01(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
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
