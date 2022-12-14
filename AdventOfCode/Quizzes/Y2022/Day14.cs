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
            var pairs = inputProvider.GetInput().Select(x => Regex.Matches(x, @"(\d+),(\d+)").Select(x => (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value))));
            (int w, int h) = FindDimensions(pairs);
            var matrix = InitializeMatrix(pairs, w, h);
            return Simulate(matrix, 500, 0);
        }

        public long Part2()
        {
            var pairs = inputProvider.GetInput().Select(x => Regex.Matches(x, @"(\d+),(\d+)").Select(x => (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value))));
            (int w, int h) = FindDimensions(pairs);
            var matrix = InitializeMatrix(pairs, w + h, h + 2, markFloor: true); // account for width and floor
            return Simulate(matrix, 500, 0);
        }

        private (int w, int h) FindDimensions(IEnumerable<IEnumerable<(int, int)>> matches)
        {
            var maxX = matches.SelectMany(x => x.Select(y => y.Item1)).Max();
            var maxY = matches.SelectMany(x => x.Select(y => y.Item2)).Max();
            return (maxX + 1, maxY + 1);
        }

        private char[][] InitializeMatrix(IEnumerable<IEnumerable<(int, int)>> matches, int w, int h, bool markFloor = false)
        {
            char[][] m = new char[h][];
            Enumerable.Range(0, h).Select(x => m[x] = new char[w]).ToList();

            foreach (var match in matches)
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

            if (markFloor) // mark last row as floor with '#'
                for (int i = 0; i < w; i++)
                    m[^1][i] = '#';

            return m;
        }

        private int Simulate(char[][] matrix, int x, int y)
        {
            (int width, int height) = (matrix[0].Length, matrix.Length);
            (int x, int y)[] dirs = new[] { (0, 1), (-1, 1), (1, 1) };

            for (int i = 0; ; i++)
            {
                (int cx, int cy) = (x, y);

                while (true)
                {
                    //condition for part 2
                    if (matrix[cy][cx] == 'o')
                        return i;

                    //abyss for part 1
                    if (cy >= height - 1 || cx - 1 <= 0 || cx + 1 >= width - 1)
                        return i;

                    var d = dirs.Select(x => (x.x + cx, x.y + cy))
                        .Where(x => IsInBounds(x.Item1, x.Item2, width, height) && matrix[x.Item2][x.Item1] == 0)
                        .FirstOrDefault();

                    if (d.Item1 != 0 && d.Item2 != 0)
                    {
                        cx = d.Item1;
                        cy = d.Item2;
                        continue;
                    }

                    matrix[cy][cx] = 'o';
                    break;
                }
            }

            throw new Exception("Should return before reaching here.");
        }

        private bool IsInBounds(int x, int y, int width, int height) => x >= 0 && x < width && y >= 0 && y < height;
    }
}
