﻿using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2015
{
    record Pnt(long X, long Y);

    public class Day03 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day03(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput().First().ToCharArray();
            return DeliverPresents(input).Count();
        }

        public long Part2()
        {
            var input = inputProvider.GetInput().First().ToCharArray();
            var set1 = input.Where((x, i) => i % 2 == 0);
            var set2 = input.Where((x, i) => i % 2 == 1);

            return DeliverPresents(set1).Union(DeliverPresents(set2)).Count();
        }

        private static HashSet<Coords2D> DeliverPresents(IEnumerable<char> directions)
        {
            var current = new Coords2D(0,0);
            var visited = new HashSet<Coords2D>() { current };

            foreach (var next in directions)
            {
                var dir = next switch
                {
                    '^' => Coords2D.Up,
                    'v' => Coords2D.Down,
                    '<' => Coords2D.Left,
                    '>' => Coords2D.Right,
                    _ => throw new Exception("invalid direction")
                };

                current += dir;
                visited.Add(current);
            }

            return visited;
        }
    }
}