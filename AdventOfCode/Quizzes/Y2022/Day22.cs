using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static QuikGraph.Algorithms.Assignment.HungarianAlgorithm;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day22 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day22(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            var lines = inputProvider.GetInput();
            var map = lines.Select(x => x.ToArray()).ToArray();
            var path = Regex.Split(lines.Last(), "(?=[RL])");
            var (pos, dir) = Simulate(map, path);

            return 1000 * pos.row + 4 * pos.col + DirScore(dir);
        }

        public long Part2()
        {
            return 0;
        }

        private Face BuildFaces(char[][] arr)
        {
            var f1 = new Face()
            {
                Data = arr[0..50].Select(x => x[50..100]).ToArray(),
                Offset = (0, 50)
            };

            var f2 = new Face() { 
                Data = arr[0..50].Select(x => x[100..150]).ToArray(),
                Offset = (0, 100)
            };

            var f3 = new Face() { 
                Data = arr[50..100].Select(x => x[50..100]).ToArray(),
                Offset = (50, 50)
            };

            var f4 = new Face() {
                Data = arr[100..150].Select(x => x[0..50]).ToArray(),
                Offset = (100, 0)
            };

            var f5 = new Face() {
                Data = arr[100..150].Select(x => x[50..100]).ToArray(),
                Offset = (100, 50)
            };

            var f6 = new Face() {
                Data = arr[150..200].Select(x => x[0..50]).ToArray(),
                Offset = (150, 0)
            };

            f1.Adjacent(f5, f3, f2, f2);
            f2.Adjacent(f2, f2, f1, f1);
            f3.Adjacent(f1, f5, f3, f3);
            f4.Adjacent(f6, f6, f5, f5);
            f5.Adjacent(f3, f1, f4, f4);
            f6.Adjacent(f4, f4, f6, f6);

            return f1;
        }

        private ((int row, int col), (int row, int col)) Simulate(char[][] map, string[] paths)
        {
            (int row, int col) dir = (0, 1);
            (int row, int col) pos = (0, 0);
            var steps = int.Parse(paths[0]);
            var face = BuildFaces(map);
            var nface = face;

            foreach (var path in paths)
            {
                if (char.IsLetter(path[0]))
                {
                    dir = ChangeDirection(path[0], dir);
                    steps = int.Parse(path[1..]);
                }

                for (int i = 0; i < steps; i++)
                {
                    nface = face;
                    (int row, int col) npos = (pos.row + dir.row, pos.col + dir.col);

                    if (npos.row < 0)
                        nface = nface.Top;

                    if (npos.row >= face.Data.Length)
                        nface = nface.Bot;

                    if (npos.col < 0)
                        nface = nface.Left;

                    if (npos.col >= 50)
                        nface = nface.Right;

                    npos = (Mod(npos.row, nface.Data.Length), Mod(npos.col, nface.Data.Length));

                    if (nface[npos.row, npos.col] == '#')
                        break;

                    face = nface;
                    pos = npos;
                }

                //Console.WriteLine((pos.col + 1 + face.Offset.col, pos.row + 1 + face.Offset.row));
            }

            return ((pos.row + face.Offset.row + 1, pos.col + face.Offset.col + 1), dir);
        }

        private (int row, int col) ChangeDirection(char dir, (int row, int col) currentDir)
        {
            return (dir, currentDir) switch
            {
                ('R', (0, 1)) => (1, 0),
                ('R', (1, 0)) => (0, -1),
                ('R', (0, -1)) => (-1, 0),
                ('R', (-1, 0)) => (0, 1),
                ('L', (0, 1)) => (-1, 0),
                ('L', (-1, 0)) => (0, -1),
                ('L', (0, -1)) => (1, 0),
                ('L', (1, 0)) => (0, 1),
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

        private class Face
        {
            public char this[int row, int col] => Data[row][col];
            public char[][] Data { get; set; }
            public (int row, int col) Offset { get; set; }
            public Face Top { get; set; }
            public Face Bot { get; set; }
            public Face Left { get; set; }
            public Face Right { get; set; }

            public void Adjacent(Face top, Face bot, Face left, Face right)
            {
                Top = top; Bot = bot; Left = left; Right = right;
            }
        }
    }
}
