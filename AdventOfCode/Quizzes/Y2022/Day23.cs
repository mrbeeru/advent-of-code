using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    [Aoc(year: 2022, day: 23)]
    public class Day23(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        private readonly (int row, int col)[] direcitons = new[] { (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1) };

        public long Part1() => Simulate(10, BuildHashSet());

        public long Part2() => Simulate(1_000_000, BuildHashSet());

        private int Simulate(int rounds, HashSet<(int row, int col)> map)
        {
            for (int round = 0; round < rounds; round++)
            {
                var dict = map.Where(x => ShouldMove(x, map))
                    .ToDictionary((elfpos) => elfpos, (elfpos) => Propose(round, elfpos, map));

                if (!dict.Any())
                    return round + 1; // part 2

                dict = dict.GroupBy(pair => pair.Value)
                         .Where(group => group.Count() == 1)
                         .Select(group => group.First())
                         .ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (var movingElves in dict)
                {
                    map.Remove(movingElves.Key);
                    map.Add(movingElves.Value);
                }
            }

            return FindEmptySpaces(map); //part 1
        }

        private int FindEmptySpaces(HashSet<(int row, int col)> map)
        {
            var minRow = map.Min(x => x.row);
            var maxRow = map.Max(x => x.row);
            var minCol = map.Min(x => x.col);
            var maxCol = map.Max(x => x.col);

            return (maxRow - minRow + 1) * (maxCol - minCol + 1) - map.Count;
        }

        private bool ShouldMove((int row, int col) pos, HashSet<(int row, int col)> elfpos)
        {
            return direcitons.Select(x => (x.row + pos.row, x.col + pos.col)).Any(elfpos.Contains);
        }

        private (int row, int col) Propose(int round, (int row, int col) pos, HashSet<(int row, int col)> map)
        {
            for (int i = 0; i < 4; i++)
            {
                var facing = (round + i) % 4;
                var dirs = GetDirections(facing).Select(x => (x.row + pos.row, x.col + pos.col));

                if (dirs.Any(map.Contains))
                    continue;

                return facing switch
                {
                    0 => (pos.row - 1, pos.col),
                    1 => (pos.row + 1, pos.col),
                    2 => (pos.row, pos.col - 1),
                    3 => (pos.row, pos.col + 1),
                    _ => throw new Exception("Illegal facing.")
                };
            }

            return (pos.row, pos.col);
        }

        private (int row, int col)[] GetDirections(int round)
        {
            return (round % 4) switch
            {
                0 => direcitons.Where(x => x.row == -1).ToArray(),
                1 => direcitons.Where(x => x.row ==  1).ToArray(),
                2 => direcitons.Where(x => x.col == -1).ToArray(),
                3 => direcitons.Where(x => x.col ==  1).ToArray(),
                _ => throw new Exception("Illegal direction.")
            };
        }

        public HashSet<(int row, int col)> BuildHashSet()
        {
            var input = inputProvider.GetInput().ToArray();
            var hashset = new HashSet<(int row, int col)>();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                        hashset.Add((i, j));
                }
            }

            return hashset;
        }
    }
}
