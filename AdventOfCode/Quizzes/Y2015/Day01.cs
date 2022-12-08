using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2015
{
    internal class Day01 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day01(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

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
