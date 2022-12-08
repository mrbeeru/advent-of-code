using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    internal class Day08 : IQuizPartOne<long>, IQuizPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day08(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

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
            return IsVisibleDown(i, j, forest) || IsVisibleUp(i, j, forest) || IsVisibleLeft(i, j, forest) || IsVisibleRight(i, j, forest);
        }

        private bool IsVisibleUp(int i, int j, int[][] forest)
        {
            for (int k = i - 1; k >= 0; k--)
            {
                if (forest[k][j] >= forest[i][j])
                    return false;
            }

            return true;
        }

        private bool IsVisibleDown(int i, int j, int[][] forest)
        {
            for (int k = i + 1; k < forest.Length; k++)
            {
                if (forest[k][j] >= forest[i][j])
                    return false;
            }

            return true;
        }

        private bool IsVisibleLeft(int i, int j, int[][] forest)
        {
            for (int k = j - 1; k >= 0; k--)
            {
                if (forest[i][k] >= forest[i][j])
                    return false;
            }

            return true;
        }

        private bool IsVisibleRight(int i, int j, int[][] forest)
        {
            for (int k = j + 1; k < forest.Length; k++)
            {
                if (forest[i][k] >= forest[i][j])
                    return false;
            }

            return true;
        }

        private int ScenicScore(int i, int j, int[][] forest)
        {
            return ScoreUp(i, j, forest) * ScoreDown(i, j, forest) * ScoreLeft(i, j, forest) * ScoreRight(i, j, forest);
        }

        private int ScoreUp(int i, int j, int[][] forest)
        {
            int k = i;
            int score = 0;

            do
            {
                k--;

                if (k < 0)
                    break;

                score++;
            } while (k >= 0 && forest[k][j] < forest[i][j]);

            return score;
        }

        private int ScoreDown(int i, int j, int[][] forest)
        {
            int k = i;
            int score = 0;

            do
            {
                k++;

                if (k >= forest.Length)
                    break;

                score++;
            } while (k < forest.Length && forest[k][j] < forest[i][j]);

            return score;
        }

        private int ScoreLeft(int i, int j, int[][] forest)
        {
            int k = j;
            int score = 0;

            do
            {
                k--;

                if (k < 0)
                    break;

                score++;
            } while (k >= 0 && forest[i][k] < forest[i][j]);

            return score;
        }

        private int ScoreRight(int i, int j, int[][] forest)
        {
            int k = j;
            int score = 0;

            do
            {
                k++;

                if (k >= forest.Length)
                    break;

                score++;
            } while (k < forest.Length && forest[i][k] < forest[i][j]);

            return score;
        }


    }
}
