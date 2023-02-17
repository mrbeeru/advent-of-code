using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2015
{
    record Pnt(long X, long Y);

    public class Day03 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day03(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput().First().ToCharArray();
            return DeliverPresents(input).Count();
        }

        public long Part2()
        {
            var input = inputProvider.GetInput().First().ToCharArray();
            var set1 = input.Where((x, i) => i % 2 == 0);
            var set2 = input.Where((x, i) => i % 2 == 1);

            return DeliverPresents(set1).Union(DeliverPresents(set2)).Count();
        }

        private HashSet<(int, int)> DeliverPresents(IEnumerable<char> directions)
        {
            var dirs = (char dir) => dir switch {
                '^' => (-1, 0),
                'v' => (1, 0),
                '<' => (0, -1),
                '>' => (0, 1),
                _ => throw new Exception("invalid direction")
            };

            var hashset = new HashSet<(int row, int col)>();
            (int row, int col) current = (0, 0);

            hashset.Add(current);

            foreach (var next in directions)
            {
                var dir = dirs(next);
                current = (current.row + dir.Item1, current.col + dir.Item2);
                hashset.Add(current);
            }

            return hashset;
        }
    }
}
