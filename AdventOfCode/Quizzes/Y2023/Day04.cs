using AdventOfCode.Extensions;
using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day04 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day04(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

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
            var lines = inputProvider.GetInput().Reverse();
            var list = new List<int>();

            foreach (var (line, i) in lines.Select((line, i) => (line, i)))
            {
                var nums = line.Nums();
                var winning = nums.Skip(1).Take(10);
                var cardNums = nums.Skip(11);
                var intersectionCount = winning.Intersect(cardNums).Count();
                var sum = list.Where((x, index) => index <= i - 1 && index > i - 1 - intersectionCount).Sum();
                list.Add(sum + 1);
            }

            return list.Sum();
        }
    }
}
