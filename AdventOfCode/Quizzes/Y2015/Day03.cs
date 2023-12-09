using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2015
{
    record Pnt(long X, long Y);

    [Aoc(year: 2015, day: 3)]
    public class Day03(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
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

        private static HashSet<Coords2D> DeliverPresents(IEnumerable<char> directions)
        {
            var current = new Coords2D(0, 0);
            var visited = new HashSet<Coords2D>() { current };

            foreach (var next in directions)
            {
                var dir = next switch
                {
                    '^' => Coords2D.Up,
                    'v' => Coords2D.Down,
                    '<' => Coords2D.Left,
                    '>' => Coords2D.Right,
                    _ => throw new Exception("invalid direction")
                };

                current += dir;
                visited.Add(current);
            }

            return visited;
        }
    }
}
