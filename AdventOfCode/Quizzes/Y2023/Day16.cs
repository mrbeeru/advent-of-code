using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 16)]
    public class Day16(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        private record Beam(Coords2D Pos, Coords2D Dir) { }

        public long Part1() => FindPathCount(new(0, 0), Coords2D.Right);

        public long Part2()
        {
            var matrix = inputProvider.GetInput().Select(x => x.ToArray()).ToArray();
            var (rows, cols) = (matrix.Length, matrix[0].Length);

            var a = Enumerable.Range(0, rows).Select(x => FindPathCount(new(x, 0), Coords2D.Right)).Max();
            var b = Enumerable.Range(0, rows).Select(x => FindPathCount(new(x, cols-1), Coords2D.Left)).Max();
            var c = Enumerable.Range(0, cols).Select(x => FindPathCount(new(0, x), Coords2D.Down)).Max();
            var d = Enumerable.Range(0, rows).Select(x => FindPathCount(new(rows-1, cols-1), Coords2D.Up)).Max();

            return new long[] { a, b, c, d }.Max();
        }

        long FindPathCount(Coords2D start, Coords2D dir)
        {
            var matrix = inputProvider.GetInput().Select(x => x.ToArray()).ToArray();
            var memo = new HashSet<Beam>();
            var beams = new List<Beam> { new(start, dir) };

            while (beams.Count > 0)
            {
                beams.ForEach(x => memo.Add(x));

                beams = beams.SelectMany(x => ChangeDir(x, matrix)).ToList();
                beams = beams.Select(x => new Beam(x.Dir + x.Pos, x.Dir)).ToList(); //next pos
                beams = beams.Where(x => x.Pos.Within(matrix) && !memo.Contains(x)).ToList();
            }

            return memo.Select(x => x.Pos).Distinct().Count();
        }

        static IList<Beam> ChangeDir(Beam beam, char[][] matrix)
        {
            if (matrix[beam.Pos.X][beam.Pos.Y] == '.')
                return [beam];

            if (beam.Dir == Coords2D.Up)
            {
                if (matrix[beam.Pos.X][beam.Pos.Y] == '|')
                {
                    return [beam];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '-')
                {
                    return [new(beam.Pos, Coords2D.Left), new(beam.Pos, Coords2D.Right)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '/')
                {
                    return [new(beam.Pos, Coords2D.Right)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '\\')
                {
                    return [new(beam.Pos, Coords2D.Left)];
                }
            }

            if (beam.Dir == Coords2D.Right)
            {
                if (matrix[beam.Pos.X][beam.Pos.Y] == '|')
                {
                    return [new(beam.Pos, Coords2D.Up), new(beam.Pos, Coords2D.Down)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '-')
                {
                    return [beam];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '/')
                {
                    return [new(beam.Pos, Coords2D.Up)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '\\')
                {
                    return [new(beam.Pos, Coords2D.Down)];
                }
            }

            if (beam.Dir == Coords2D.Down)
            {
                if (matrix[beam.Pos.X][beam.Pos.Y] == '|')
                {
                    return [beam];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '-')
                {
                    return [new(beam.Pos, Coords2D.Left), new(beam.Pos, Coords2D.Right)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '/')
                {
                    return [new(beam.Pos, Coords2D.Left)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '\\')
                {
                    return [new(beam.Pos, Coords2D.Right)];
                }
            }

            if (beam.Dir == Coords2D.Left)
            {
                if (matrix[beam.Pos.X][beam.Pos.Y] == '|')
                {
                    return [new(beam.Pos, Coords2D.Up), new(beam.Pos, Coords2D.Down)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '-')
                {
                    return [beam];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '/')
                {
                    return [new(beam.Pos, Coords2D.Down)];
                }

                if (matrix[beam.Pos.X][beam.Pos.Y] == '\\')
                {
                    return [new(beam.Pos, Coords2D.Up)];
                }
            }

            throw new Exception("Invalid beam direction or map instruction");
        }
    }
}
