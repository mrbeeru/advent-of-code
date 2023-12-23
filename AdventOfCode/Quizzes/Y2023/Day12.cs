using AdventOfCode.Extensions;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 12)]
    public class Day12(IInputProvider inputProvider) : IPartOne<long>
    {
        private record Spring(char[] Layout, int[] Groups);

        public long Part1()
        {
            var input = inputProvider.GetInput().Select(x => new Spring([.. x.Split(" ")[0]], x.Split(" ")[1].Nums().ToArray())).ToList();
            var sum = 0;

            input.ForEach(x => Recurse(x.Layout, 0, x.Groups, 0, ref sum));
            return sum;
        }

        static void Recurse(char[] input, int index, int[] nums, int numIndex, ref int sum)
        {
            // skip spaces;
            while (index < input.Length && input[index] == '.')
                index++;

            // finished
            if (index >= input.Length)
                return;

            if (input[index] == '?')
                Recurse(input, index + 1, nums, numIndex, ref sum);

            var num = nums[numIndex];

            for (int i = 0; i < num; i++)
            {
                // bad, outside
                if (i + index >=  input.Length)
                    return;

                //bad, need to place #
                if (input[i + index] == '.')
                    return;
            }

            index += num;

            if (numIndex == nums.Length - 1)
            {
                while (index < input.Length)
                {
                    //bad, need all to be . or ?
                    if (input[index] == '#')
                    {
                        return;
                    }
                    index++;
                }
                sum++;
                return;
            }

            if (index <  input.Length && input[index] == '#')
            {
                //bad, need to separate ###
                return;
            }

            // pick next num
            Recurse(input, index + 1, nums, numIndex + 1, ref sum);
        }
    }
}
