using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day15 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day15(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput()
                .Select(x => x.Nums().Chunk(2).ToArray()).ToArray();

            int k = 2000000;

            //select (full distance, distance on Y to Y = k) 
            IEnumerable<(int x, int d, int dy)> dists = input.Select(coords => (coords[0][0],Math.Abs(coords[0][0] - coords[1][0]) + Math.Abs(coords[0][1] - coords[1][1]), Math.Abs(coords[0][1] - k)));
            var overlapsK = new List<(int, int)>();

            foreach(var dist in dists)
            {
                // d = 9 dy = 3 x = 8
                // 9 - 3 = 6
                // t = 6 * 2 + 1
                // x1 = x - 6 x2 = x + 6

                if (dist.d >= dist.dy)
                {
                    (int x1, int x2) line = (dist.x - (dist.d - dist.dy), dist.x + (dist.d - dist.dy));
                    overlapsK.Add(line);
                }
            }

            (int x1, int x2) = (overlapsK.Select(x => x.Item1).Min(), overlapsK.Select(x => x.Item2).Max());
            return x2 - x1;
        }

        private ((int, int), (int, int)) FindDimensions(int[][][] input)
        {
            int maxX = input.Select(x => x[0][0] > x[1][0] ? x[0][0] : x[1][0]).Max();
            int maxY = input.Select(x => x[0][1] > x[1][1] ? x[0][1] : x[1][1]).Max();
            int minX = input.Select(x => x[0][0] < x[1][0] ? x[0][0] : x[1][0]).Min();
            int minY = input.Select(x => x[0][1] < x[1][1] ? x[0][1] : x[1][1]).Min();

            return ((minX, minY), (maxX, maxY));
        }
    }
}
