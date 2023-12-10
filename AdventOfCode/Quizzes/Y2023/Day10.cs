using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 10)]
    public class Day10(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        // too lazy to auto detect which way is the loop
        // these should be adapted for unit tests or different inputs
        // start pipe (S)
        public char Pipe = '-';

        // the right side of the direction should point towards the loop interior
        public Coords2D Dir = Coords2D.Right;

        readonly Dictionary<(char, Coords2D), Coords2D> dirMap = new() {
            {('-', Coords2D.Right), Coords2D.Right},
            {('J', Coords2D.Right), Coords2D.Up},
            {('7', Coords2D.Right), Coords2D.Down},
            {('-', Coords2D.Left), Coords2D.Left},
            {('F', Coords2D.Left), Coords2D.Down},
            {('L', Coords2D.Left), Coords2D.Up},
            {('|', Coords2D.Down), Coords2D.Down},
            {('J', Coords2D.Down), Coords2D.Left},
            {('L', Coords2D.Down), Coords2D.Right},
            {('|', Coords2D.Up), Coords2D.Up},
            {('7', Coords2D.Up), Coords2D.Left},
            {('F', Coords2D.Up), Coords2D.Right},
        };

        public long Part1() => Solve().steps;
        public long Part2() => Solve().innerTiles;

        public (long steps, long innerTiles) Solve()
        {
            var (matrix, startPos) = BuildMatrixAndFindS();
            var (currentPos, pipe, dir, steps) = (startPos, Pipe, Dir, 0);
            var floodPoints = new List<Coords2D>();

            do
            {
                floodPoints.AddRange(CalcFloodPoint(currentPos, dir, pipe));
                matrix[currentPos.X, currentPos.Y] = '#';
                dir = dirMap[(pipe, dir)];
                currentPos += dir;
                pipe = matrix[currentPos.X, currentPos.Y];
                steps++;
            }
            while (currentPos != startPos);

            var innerTiles = 0;
            floodPoints.ForEach(fp => Flood(matrix, fp, ref innerTiles));

            return (steps / 2, innerTiles);
        }

        static Coords2D[] CalcFloodPoint(Coords2D pos, Coords2D dir, char pipe)
        {
            return (pipe, dir) switch
            {
                ('F', (0, -1)) => new[] { pos + Coords2D.Left, pos + Coords2D.Up, pos + Coords2D.Left + Coords2D.Up },
                ('L', (1, 0)) => new[] { pos + Coords2D.Down, pos + Coords2D.Left, pos + Coords2D.Down + Coords2D.Left },
                ('J', (0, 1)) => new[] { pos + Coords2D.Right, pos + Coords2D.Down, pos + Coords2D.Right + Coords2D.Down },
                ('7', (-1, 0)) => new[] { pos + Coords2D.Up, pos + Coords2D.Right, pos + Coords2D.Up + Coords2D.Right },
                (_, (-1, 0)) => new[] { pos + Coords2D.Right },
                (_, (0, 1)) => new[] { pos + Coords2D.Down },
                (_, (1, 0)) => new[] { pos + Coords2D.Left },
                (_, (0, -1)) => new[] { pos + Coords2D.Up },
                _ => throw new Exception("Invalid direction.")
            };
        }

        static void Flood(char[,] matrix, Coords2D pos, ref int sum)
        {
            if (!pos.Within(matrix) || matrix[pos.X, pos.Y] == '#')
                return;

            matrix[pos.X, pos.Y] = '#';
            sum++;

            foreach (var dir in Coords2D.Directions)
                Flood(matrix, dir + pos, ref sum);
        }

        (char[,], Coords2D startPos) BuildMatrixAndFindS()
        {
            var matrix = inputProvider.GetInput().Select(x => x.ToArray()).ToArray();
            var output = new char[matrix.Length, matrix[0].Length];
            Coords2D startPos = new(0, 0);

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    output[i, j] = matrix[i][j];
                    if (output[i, j] == 'S')
                        startPos = new(i, j);
                }
            }

            return (output, startPos);
        }
    }
}
