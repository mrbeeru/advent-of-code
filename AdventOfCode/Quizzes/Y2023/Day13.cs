using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 14)]
    public class Day14(IInputProvider inputProvider) : IPartOne<long>
    {
        public long Part1()
        {
            var matrix = inputProvider.GetInput().Select(x => x.ToArray()).ToArray();
            Tilt(matrix, Coords2D.Up);
            return Sum(matrix);
        }

        static void Tilt(char[][] matrix, Coords2D dir)
        {

            if (dir == Coords2D.Up)
            {
                for (var i = 0; i < matrix.Length; i++)
                {
                    for (var j = 0; j < matrix[i].Length; j++)
                    {
                        if (matrix[i][j] == 'O')
                        {
                            for (var k = i - 1; k >= 0; k--)
                            {
                                if (matrix[k][j] == '.')
                                {
                                    matrix[k][j] = 'O';
                                    matrix[k + 1][j] = '.';
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (dir == Coords2D.Left)
            {
                for (var i = 0; i < matrix.Length; i++)
                {
                    for (var j = 0; j < matrix[i].Length; j++)
                    {
                        if (matrix[i][j] == 'O')
                        {
                            for (var k = j - 1; j >= 0; k--)
                            {
                                if (matrix[i][k] == '.')
                                {
                                    matrix[i][k] = 'O';
                                    matrix[i][j + 1] = '.';
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (dir == Coords2D.Down)
            {
                for (var i = matrix.Length - 1; i >= 0; i--)
                {
                    for (var j = matrix[i].Length - 1; j >= 0; j--)
                    {
                        if (matrix[i][j] == 'O')
                        {
                            for (var k = i + 1; k < matrix.Length; k++)
                            {
                                if (matrix[k][j] == '.')
                                {
                                    matrix[k][j] = 'O';
                                    matrix[k - 1][j] = '.';
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (dir == Coords2D.Right)
            {
                for (var i = matrix.Length - 1; i >= 0; i--)
                {
                    for (var j = matrix[i].Length - 1; j >= 0; j--)
                    {
                        if (matrix[i][j] == 'O')
                        {
                            for (var k = j + 1; k < matrix[i].Length; k++)
                            {
                                if (matrix[i][k] == '.')
                                {
                                    matrix[i][k] = 'O';
                                    matrix[i][j - 1] = '.';
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static long Sum(char[][] matrix)
        {
            long sum = 0;
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 'O')
                    {
                        sum += (matrix.Length - i);
                    }
                }
            }

            return sum;
        }
    }
}
