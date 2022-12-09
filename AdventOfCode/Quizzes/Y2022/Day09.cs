using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/9
    /// </summary>
    internal class Day09 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day09(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();
            return Solve(input, Enumerable.Range(0, 2).Select(x => (0, 0)).ToArray());
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();
            return Solve(input, Enumerable.Range(0, 10).Select(x => (0, 0)).ToArray());
        }

        private long Solve(IEnumerable<string> input, (int x, int y)[] knotPositions)
        {
            var tailMoveHistory = new List<(int, int)>();
            input.Select(x => x.Split(" ")).ForEach(y => MoveRope(y[0], int.Parse(y[1]), knotPositions, tailMoveHistory));

            return tailMoveHistory.Distinct().Count();
        }

        public void MoveRope(string dir, int numTimes, (int x, int y)[] knotPositions, List<(int x, int y)> tailMoveHistory)
        {
            var direction = FindMoveDirection(dir);

            for (int i = 0; i < numTimes; i++)
            {
                var head = knotPositions[0];
                head.x += direction.x;
                head.y += direction.y;
                knotPositions[0] = head;

                for (int j = 1; j < knotPositions.Length; j++)
                {
                    var knot = knotPositions[j];
                    var knotDir = FindKnotDirection(head, knot);

                    knot.x += knotDir.x;
                    knot.y += knotDir.y;
                    knotPositions[j] = knot;
                    head = knotPositions[j];
                }

                tailMoveHistory.Add(knotPositions.Last());
            }
        }

        public (int x, int y) FindMoveDirection(string dir)
        {
            return dir switch
            {
                "U" => (0, -1),
                "D" => (0, 1),
                "R" => (1, 0),
                "L" => (-1, 0),
                _ => throw new Exception("Illegal move.")
            };
        }

        public (int x, int y) FindKnotDirection((int x, int y) head, (int x, int y) tail)
        {
            (int x, int y) = (tail.x - head.x, tail.y - head.y);
            var distance = Max(Abs(x), Abs(y));

            return distance != 2 ? (0, 0) : (-x / Max(Abs(x), 1), -y / Max(Abs(y), 1));
        }
    }
}
