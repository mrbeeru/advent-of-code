using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2015
{
    [Aoc(year: 2015, day: 1)]
    public class Day01(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput().First();
            return input.Count(x => x == '(') - input.Count(x => x == ')');
        }

        public long Part2()
        {
            var input = inputProvider.GetInput().First();
            int sum = 0, index = 0;

            foreach (var c in input)
            {
                sum += c == '(' ? 1 : -1;
                index++;

                if (sum < 0)
                    break;
            }

            return index;
        }
    }
}
