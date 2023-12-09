using AdventOfCode.Extensions;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day18 : IPartOne<long>, IPartTwo<long>
    {
        readonly IInputProvider inputProvider;
        readonly (int x, int y, int z)[] dirs = new[] { (-1, 0, 0), (1, 0, 0), (0, -1, 0), (0, 1, 0), (0, 0, -1), (0, 0, 1) };
        readonly HashSet<(int, int, int)> visited = new();
        HashSet<(int, int, int)> input = new();
        (int x, int y, int z) min;
        (int x, int y, int z) max;

        public Day18(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            input = inputProvider.GetInput().Select(x => x.Nums().ToArray()).Select(x => (x[0], x[1], x[2])).ToHashSet();
            var visited = new HashSet<(int x, int y, int z)>();
            var score = 0;

            foreach (var cube in input)
            {
                var adjCubes = dirs.Select(x => (x.Item1 + cube.Item1, x.Item2 + cube.Item2, x.Item3 + cube.Item3));
                var neighbours = visited.Intersect(adjCubes);
                visited.Add(cube);

                score += 6 - 2 * neighbours.Count();
            }

            return score;
        }

        public long Part2()
        {
            input = inputProvider.GetInput().Select(x => x.Nums().ToArray()).Select(x => (x[0], x[1], x[2])).ToHashSet();

            (var minX, var maxX) = (input.Select(x => x.Item1).Min() - 1, input.Select(x => x.Item1).Max() + 1);
            (var minY, var maxY) = (input.Select(x => x.Item2).Min() - 1, input.Select(x => x.Item2).Max() + 1);
            (var minZ, var maxZ) = (input.Select(x => x.Item3).Min() - 1, input.Select(x => x.Item3).Max() + 1);

            var space = Space((minX, minY, minZ), (maxX, maxY, maxZ));

            min = (minX, minY, minZ);
            max = (maxX, maxY, maxZ);

            DFS(min);
            var inner = space.Except(visited);
            var innerAir = inner.Except(input);
            var innerCubes = inner.Intersect(input);
            var innerCubeFaces = innerAir.SelectMany(air => dirs.Select(dir => (dir.x + air.Item1, dir.y + air.Item2, dir.z + air.Item3)))
                .Where(x => input.Contains(x));

            return Part1() - innerCubeFaces.Count();
        }

        private void DFS((int x, int y, int z) current)
        {
            IEnumerable<(int x, int y, int z)> nextCoords = dirs.Select(x => (current.x + x.x, current.y + x.y, current.z + x.z));

            foreach (var next in nextCoords)
            {
                if (visited.Contains(next))
                    continue;

                if (next.x < min.x || next.y < min.y || next.z < min.z || next.x > max.x || next.y > max.y || next.z > max.z)
                    continue;

                visited.Add(next);

                if (!input.Contains(next))
                    DFS(next);
            }
        }

        private HashSet<(int x, int y, int z)> Space((int x, int y, int z) min, (int x, int y, int z) max)
        {
            var output = new HashSet<(int, int, int)>();

            for (int x = min.x; x <= max.x; x++)
            {
                for (int y = min.y; y <= max.y; y++)
                {
                    for (var z = min.z; z <= max.z; z++)
                    {
                        output.Add((x, y, z));
                    }
                }
            }

            return output;
        }
    }
}
