using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/6
    /// </summary>
    [Aoc(year: 2022, day: 6)]
    public class Day06(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            return FindMarkerIndex(inputProvider.GetInput().First(), 4);
        }

        public long Part2()
        {
            return FindMarkerIndex(inputProvider.GetInput().First(), 14);
        }

        private int FindMarkerIndex(string input, int count)
        {
            return input.Window(count)
                .Select((chars, index) => (chars.GroupBy(c => c), index))
                .First(x => x.Item1.Count() == count).index + count;
        }
    }
}
