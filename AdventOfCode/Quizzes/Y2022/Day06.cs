using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/6
    /// </summary>
    public class Day06 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day06(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            //1 line input
            return FindMarkerIndex(inputProvider.GetInput().First(), 4);
        }

        public long Part2()
        {
            //1 line input
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
