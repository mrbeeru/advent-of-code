using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day22 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day22(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            var lines = inputProvider.GetInput();
            var map = BuildMap(lines);
            var path = Regex.Split(lines.Last(), "(?=[RL])");
            (int row, int col) start = FindStartPosition(map);

            var (pos, dir) = Simulate(map, path, start);

            return 1000 * (pos.row + 1) + 4 * (pos.col + 1) + DirScore(dir);
        }

        private ((int row, int col), (int row, int col)) Simulate(char[,] map, string[] paths, (int row, int col) start)
        {
            (int row, int col) dir = (0, 1);
            //var ndir = dir;
            var steps = int.Parse(paths[0]);
            var pos = start;

            foreach (var path in paths)
            {
                if (char.IsLetter(path[0]))
                {
                    dir = ChangeDirection(path[0], dir);
                    steps = int.Parse(path[1..]);
                }

                for (int i = 0; i < steps; i++)
                {
                    (int row, int col) npos = (pos.row + dir.row, pos.col + dir.col);
                    npos = (Mod(npos.row, map.GetLength(0)), Mod(npos.col, map.GetLength(1)));

                    if (map[npos.row, npos.col] == '#')
                        break;

                    if (map[npos.row, npos.col] == ' ')
                    {
                        var t = WrapAround(map, npos, dir);
                        if (t != npos)
                        {
                            pos = t;
                        }

                        continue;
                    }

                    pos = npos;
                }

                Console.WriteLine((pos.col + 1, pos.row + 1));
            }

            return (pos, dir);
        }

        private (int row, int col) WrapAround(char[,] map, (int row, int col) pos, (int row, int col) dir)
        {
            var next = pos;
            
            while (map[next.row, next.col] == ' ')
            {
                next = (next.row + dir.row, next.col + dir.col);
                next = (Mod(next.row, map.GetLength(0)), Mod(next.col, map.GetLength(1)));
            }

            if (map[next.row, next.col] == '.')
                pos = next;

            return pos;
        }

        private (int row, int col) ChangeDirection(char dir, (int row, int col) currentDir)
        {
            return (dir, currentDir) switch
            {
                ('R', ( 0,  1)) => ( 1,  0),
                ('R', ( 1,  0)) => ( 0, -1),
                ('R', ( 0, -1)) => (-1,  0),
                ('R', (-1,  0)) => ( 0,  1),
                
                ('L', ( 0,  1)) => (-1,  0),
                ('L', (-1,  0)) => ( 0, -1),
                ('L', ( 0, -1)) => ( 1,  0),
                ('L', ( 1,  0)) => ( 0,  1),

                _ => throw new Exception("Invalid direction.")
            };
        }

        private int DirScore((int row, int col) currentDir)
        {
            return currentDir switch
            {
                (0, 1) => 0,
                (-1, 0) => 1,
                (0, -1) => 2,
                (1, 0) => 3,
                _ => throw new Exception("Invalid direction.")
            };
        }

        private int Mod(int x, int m) => (x%m + m)%m;

        private char[,] BuildMap(IEnumerable<string> input)
        {
            var lines = input.ToArray()[0..^2];
            var rows = lines.Count();
            var cols = lines.Select(x => x.Length).Max();

            var map = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (j < lines.ElementAt(i).Count())
                        map[i, j] = lines[i][j];
                    else
                        map[i, j] = ' ';
                    
                }
            }

            return map;
        }

        private void PrintMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                };
                Console.WriteLine();
            }
        }

        private (int row, int col) FindStartPosition(char[,] map)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[0, j] == '.')
                    return (0, j);
            }

            throw new Exception("Start positin not found.");
        }
    }
}
