using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2015
{
    /// <summary>
    /// https://adventofcode.com/2015/day/2
    /// </summary>
    [Aoc(year: 2015, day: 2)]
    public class Day02(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput();

            // sums the surface areas of all the rectangular prisms
            return input.Select(x => x.Split('x').Select(y => int.Parse(y)).OrderBy(y => y).ToArray())
                .Select(dim => 2 * (dim[0] * dim[1] + dim[1] * dim[2] + dim[2] * dim[0]) + dim[0] * dim[1])
                .Sum();
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();

            // sums the lengths of ribbons of all the rectangular prisms
            return input.Select(x => x.Split('x').Select(y => int.Parse(y)).OrderBy(y => y).ToArray())
                .Select(x => x[0] + x[0] + x[1] + x[1] + x[0]*x[1]*x[2])
                .Sum();
        }
    }
}
