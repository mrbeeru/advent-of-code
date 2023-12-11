using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 11)]
    public class Day11(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1() => Solve(2);

        public long Part2() => Solve(1_000_000);

        long Solve(int space)
        {
            var matrix = inputProvider.GetInput().Select(x => x.ToArray()).ToArray();
            var (galaxies, rowDict, colDict) = FindEmptyRowsColsAndGalaxies(matrix);
            var sum = 0L;

            for (var i = 0; i < galaxies.Count; i++)
            {
                var start = galaxies[i];
                for (var j = i + 1; j < galaxies.Count; j++)
                {
                    var end = galaxies[j];
                    var rows = rowDict.Count(x => x.Within(start.X, end.X));
                    var cols = colDict.Count(x => x.Within(start.Y, end.Y));
                    sum += Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y) + space * cols + space * rows - cols - rows;
                }
            }

            return sum;
        }

        static (List<Coords2D> galaxies, List<int> rowDict, List<int> colDict) FindEmptyRowsColsAndGalaxies(char[][] matrix)
        {
            var emptyCols = new List<int>();
            var emptyRows = new List<int>();
            var galaxies = new List<Coords2D>();

            for (int i = 0; i < matrix.Length; i++)
            {
                bool flag = true;

                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == '#')
                    {
                        galaxies.Add(new(i, j));
                        flag = false;
                    }
                }

                if (flag)
                {
                    emptyRows.Add(i);
                }

                flag = true;

                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[j][i] == '#')
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    emptyCols.Add(i);
                }
            }

            return (galaxies, emptyRows, emptyCols);
        }
    }
}
