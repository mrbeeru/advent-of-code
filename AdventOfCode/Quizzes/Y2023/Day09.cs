using AdventOfCode.Extensions;
using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day09 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day09(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1() => Solve(part: 1);

        public long Part2() => Solve(part: 2);

        long Solve(int part)
        {
            var nums = inputProvider.GetInput().Select(x => x.Nums().ToArray());
            var totalSum = 0L;

            foreach (var row in nums)
            {
                var matrix = BuildMatrix(row);
                totalSum += CalcHistory(matrix, part);
            }

            return totalSum;
        }

        static long CalcHistory(long[,] matrix, int part)
        {
            long sum = 0L, len = matrix.GetLength(0) - 1;

            for (long i = len; i >= 0; i--)
            {
                sum  = part == 2 ? -sum : sum;
                sum += part == 2 ? matrix[i, i] : matrix[i, len];
            }

            return sum;
        }

        static long[,] BuildMatrix(int[] nums)
        {
            var matrix = new long[nums.Length, nums.Length];

            for (int i = 0; i <nums.Length; i++)
                matrix[0, i] = nums[i];

            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = i; j < nums.Length; j++)
                {
                    matrix[i, j] = matrix[i - 1, j] - matrix[i - 1, j - 1];
                }
            }

            return matrix;
        }
    }
}
