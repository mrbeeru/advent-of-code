using AdventOfCode.Extensions;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 9)]
    public class Day09(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1() => Solve((nums) => nums);

        public long Part2() => Solve((nums) => nums.Reverse());

        long Solve(Func<IEnumerable<int>, IEnumerable<int>> fn)
        {
            var nums = inputProvider.GetInput().Select(x => fn(x.Nums()).ToArray());
            var totalSum = 0L;

            foreach (var row in nums)
            {
                var matrix = BuildMatrix(row);
                totalSum += CalcHistory(matrix);
            }

            return totalSum;
        }

        static long CalcHistory(long[,] matrix)
        {
            long sum = 0L, len = matrix.GetLength(0) - 1;

            for (long i = len; i >= 0; i--)
                sum += matrix[i, len];

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
