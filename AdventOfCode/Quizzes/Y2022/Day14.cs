using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using MoreLinq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day14 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day14(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var matrix = InitializeMatrix();
            return Simulate(matrix, 500, 0);
        }

        public long Part2()
        {
            var matrix = InitializeMatrix(isPart2: true);
            return Simulate(matrix, 500, 0);
        }

        private char[][] InitializeMatrix(bool isPart2 = false)
        {
            var pairs = inputProvider.GetInput().Select(x => Regex.Matches(x, @"(\d+),(\d+)").Select(x => (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value))));
            (int w, int h) = FindDimensions(pairs, isPart2);

            char[][] m = new char[h][];
            Enumerable.Range(0, h).Select(x => m[x] = new char[w]).ToList();

            foreach (var match in pairs)
            {
                (int x, int y) current = match.First();

                foreach (var end in match.Skip(1))
                {
                    while (current != end)
                    {
                        m[current.y][current.x] = '#';
                        current = (current.x + Math.Sign(end.Item1 - current.x), current.y + Math.Sign(end.Item2 - current.y));
                    }

                    m[end.Item2][end.Item1] = '#';
                }
            }

            if (isPart2) // mark last row as floor with '#'
                for (int i = 0; i < w; i++)
                    m[^1][i] = '#';

            return m;
        }

        private (int w, int h) FindDimensions(IEnumerable<IEnumerable<(int, int)>> matches, bool isPart2)
        {
            var maxX = matches.SelectMany(x => x.Select(y => y.Item1)).Max();
            var maxY = matches.SelectMany(x => x.Select(y => y.Item2)).Max();
            return (maxX + 1 + (isPart2 ? maxY : 0), maxY + 1 + (isPart2 ? 2 : 0));
        }

        private int Simulate(char[][] matrix, int x, int y)
        {
            (int width, int height) = (matrix[0].Length, matrix.Length);
            (int x, int y)[] dir = new[] { (0, 1), (-1, 1), (1, 1) };

            for (int i = 0; ; i++)
            {
                (int col, int row) = (x, y);

                while (true)
                {
                    // either sand fell in abyss or built up to the source, we can stop here.
                    if (row >= height - 1 || col - 1 <= 0 || col + 1 >= width - 1 || matrix[row][col] == 'o')
                        return i;

                    var d = dir.Select(x => (x.y + row, x.x + col))
                        .Where(x => (x.Item1, x.Item2).Within(matrix) && matrix[x.Item1][x.Item2] == 0)
                        .FirstOrDefault();

                    // this means we can't go in any direction => sand found it's resting place
                    if (d.Item1 == 0 && d.Item2 == 0)
                    {
                        matrix[row][col] = 'o';
                        break;
                    }

                    (row, col) = (d.Item1, d.Item2);
                }
            }

            throw new Exception("Should return before reaching here.");
        }
    }
}