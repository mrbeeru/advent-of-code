﻿using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/8
    /// </summary>
    [Aoc(year: 2022, day: 8)]
    public class Day08(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var forest = inputProvider.GetInput().Select(x => x.Select(y => y - '0').ToArray()).ToArray();
            return forest.SelectMany(x => x).Where((x, i) => IsVisible(i / forest.Length, i % forest.Length, forest)).Count();
        }

        public long Part2()
        {
            var forest = inputProvider.GetInput().Select(x => x.Select(y => y - '0').ToArray()).ToArray();
            return forest.SelectMany(x => x).Select((x, i) => ScenicScore(i / forest.Length, i % forest.Length, forest)).Max();
        }

        private bool IsVisible(int i, int j, int[][] forest)
        {
            (int, int)[] directions = new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };
            return directions.Any(d => IsVisibleDirection(i, j, forest, d));
        }

        private bool IsVisibleDirection(int a, int b, int[][] forest, (int x, int y) direction)
        {
            var i = a + direction.x;
            var j = b + direction.y;

            while ((i, j).Within(forest))
            {
                if (forest[i][j] >= forest[a][b])
                    return false;

                i += direction.x;
                j += direction.y;
            }

            return true;
        }

        private int ScenicScore(int i, int j, int[][] forest)
        {
            (int, int)[] directions = new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };
            return directions.Aggregate(1, (a, b) => a *  ScenicScoreDirection(i, j, forest, b));
        }

        private int ScenicScoreDirection(int a, int b, int[][] forest, (int x, int y) direction)
        {
            var i = a;
            var j = b;

            int score = 0;

            do
            {
                i += direction.x;
                j += direction.y;

                if (!(i, j).Within(forest))
                    break;

                score++;
            } while ((i, j).Within(forest) && forest[i][j] < forest[a][b]);

            return score;
        }
    }
}
