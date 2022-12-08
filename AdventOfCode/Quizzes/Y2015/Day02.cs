using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2015
{
    /// <summary>
    /// https://adventofcode.com/2015/day/2
    /// </summary>
    internal class Day02 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day02(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

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
