using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day15 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;
        public int K { get; set; } = 2_000_000; // used to pass value 10 in unit testing

        public Day15(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1() => Solve(K, K, isPart1: true);

        public long Part2() => Solve(0, 4_000_000, isPart1: false);

        public long Solve(int start, int stop, bool isPart1 = true)
        {
            var input = inputProvider.GetInput()
               .Select(x => x.Nums().Chunk(2).ToArray()).ToArray();

            var result = 0L;

            Parallel.For(start, stop + 1, (k, state) =>
            {
                var linesK = JoinLines(FindLines(input, k).OrderBy(x => x.Item1));

                if (!linesK.Any())
                    return;

                if (isPart1 == true)
                {
                    result = linesK.Sum(x => x.Item2 - x.Item1);
                    state.Stop();
                }
                else
                {
                    if (linesK.Count() > 1)
                    {
                        result = 4_000_000L * (linesK.First().Item2 + 1) + k;
                        state.Stop();
                    }
                }
            });

            return result;
        }

        private IEnumerable<(int, int)> FindLines(int[][][] input, int k)
        {
            //select (sensor x, sensor max distance, delta between k and sensor y) 
            IEnumerable<(int x, int d, int dy)> dists = input.Select(coords => (coords[0][0], Math.Abs(coords[0][0] - coords[1][0]) + Math.Abs(coords[0][1] - coords[1][1]), Math.Abs(coords[0][1] - k)));
            var lines = new List<(int, int)>();

            foreach (var dist in dists)
            {
                if (dist.d >= dist.dy)
                {
                    (int x1, int x2) line = (dist.x - (dist.d - dist.dy), dist.x + (dist.d - dist.dy));
                    lines.Add(line);
                }
            }

            return lines;
        }

        private IEnumerable<(int, int)> JoinLines(IEnumerable<(int, int)> lineSegments)
        {
            var output = new List<(int, int)>();

            if (!lineSegments.Any())
                return output;

            lineSegments = lineSegments.OrderBy(x => x.Item1);
            var current = lineSegments.First();

            foreach (var next in lineSegments.Skip(1))
            {
                if (next.Item1 > current.Item2)
                {
                    // we have a break
                    output.Add(current);
                    current = next;
                }
                else
                    current = (current.Item1, Math.Max(current.Item2, next.Item2));
            }

            output.Add(current);
            return output;
        }
    }
}
