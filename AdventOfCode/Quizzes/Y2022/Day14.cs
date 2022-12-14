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
    public class Day14 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day14(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();
            var matches = input.Select(x => Regex.Matches(x, @"(\d+),(\d+)").Select(x => (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value))));
            (int w, int h) = FindDimensions(matches);
            var matrix = InitializeMatrix(matches, w, h);
            var result = Simulate(matrix, 500, 0);
            DisplayMatrix(matrix);

            return result;
        }

        public (int w, int h) FindDimensions(IEnumerable<IEnumerable<(int, int)>> matches)
        {
            //var minX = matches.SelectMany(x => x.Select(y => y.Item1)).Min();
            var maxX = matches.SelectMany(x => x.Select(y => y.Item1)).Max();
            var maxY = matches.SelectMany(x => x.Select(y => y.Item2)).Max();
            return (maxX + 1, maxY + 1);
        }

        public char[][] InitializeMatrix(IEnumerable<IEnumerable<(int, int)>> matches, int w, int h)
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

            return m;
        }

        public void DisplayMatrix(char[][] matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    Console.Write(col == 0 ? ' ' : col);
                }

                Console.WriteLine(); 
            }
        }

        public int Simulate(char[][] matrix, int x, int y)
        {
            int i = 0;
            for (; i < 1000000; i++)
            {
                (int cx, int cy) = (x, y);

                while (true)
                {
                    // try go bottom
                    if (cy + 1 < matrix.Length && matrix[cy + 1][cx] == 0)
                    {
                        cy++;
                        continue;
                    }

                    // bottom left
                    if (cx - 1 >= 0 && cy + 1 < matrix.Length && matrix[cy+1][cx-1] == 0)
                    {
                        cx--;
                        cy++;
                        continue;
                    }

                    // bottom right
                    if (cx + 1 < matrix[0].Length && cy + 1 < matrix.Length && matrix[cy + 1][cx + 1] == 0)
                    {
                        cx++;
                        cy++;
                        continue;
                    }

                    //abyss
                    if (cy >= matrix.Length - 1)
                        return i;

                    if (cx - 1 <= 0)
                        return i;

                    if (cx + 1 >= matrix[0].Length - 1)
                        return i;

                    matrix[cy][cx] = 'o';
                    break;
                }

            }

            return i;
        }

    }
}
