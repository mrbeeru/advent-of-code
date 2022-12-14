using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public (int w, int h) FindDimensions(IEnumerable<IEnumerable<(int, int)>> matches)
        {
            var maxX = matches.SelectMany(x => x.Select(y => y.Item1)).Max();
            var maxY = matches.SelectMany(x => x.Select(y => y.Item2)).Max();
            return (maxX + 1, maxY + 1);
        }

        public char[][] InitializeMatrix(IEnumerable<IEnumerable<(int, int)>> matches, int w, int h, bool markFloor = false)
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

        public int Simulate(char[][] matrix, int x, int y)
        {
            (int width, int height) = (matrix[0].Length, matrix.Length);

            for (int i = 0; ; i++)
            {
                (int cx, int cy) = (x, y);

                while (true)
                {
                    if (matrix[cy][cx] == 'o')
                        return i;

                    //abyss
                    if (cy >= height - 1)
                        return i;

                    if (cx - 1 <= 0)
                        return i;

                    if (cx + 1 >= width - 1)
                        return i;

                    // try go bottom
                    if (cy + 1 < height && matrix[cy + 1][cx] == 0)
                    {
                        cy++;
                        continue;
                    }

                    // bottom left
                    if (cx - 1 >= 0 && cy + 1 < height && matrix[cy+1][cx-1] == 0)
                    {
                        cx--; cy++;
                        continue;
                    }

                    // bottom right
                    if (cx + 1 < width && cy + 1 < height && matrix[cy + 1][cx + 1] == 0)
                    {
                        cx++; cy++;
                        continue;
                    }

                    matrix[cy][cx] = 'o';
                    break;
                }
            }

            throw new Exception("Should return before reaching here.");
        }
    }
}
