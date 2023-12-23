using AdventOfCode.Extensions;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 12)]
    public class Day12(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        private record Spring(char[] Layout, int[] Groups);

        public long Part1()
        {
            var input = inputProvider.GetInput().Select(x => new Spring([.. x.Split(" ")[0]], x.Split(" ")[1].Nums().ToArray())).ToList();
            var sum = 0;
            var sum2 = 0;

            input.ForEach(x => Recurse(x.Layout, 0, x.Groups, 0, ref sum, ref sum2));
            return sum;
        }

        public long Part2()
        {
            var input = inputProvider.GetInput().Select(x => new Spring([.. x.Split(" ")[0]], x.Split(" ")[1].Nums().ToArray())).ToList();
            var sum = 0;
            var test = 0;

            foreach (var spring in input)
            {
                char[] layoutUnfolded = [.. spring.Layout, '?', .. spring.Layout, '?', .. spring.Layout, '?', .. spring.Layout, '?', .. spring.Layout];
                int[] groupUnfolded = [.. spring.Groups, .. spring.Groups, .. spring.Groups, .. spring.Groups, .. spring.Groups];
                Console.WriteLine(string.Join("", spring.Layout));
                Recurse(layoutUnfolded, 0, groupUnfolded, 0, ref sum, ref test);
            }

            return sum;
        }

        static void Recurse(char[] input, int index, int[] nums, int numIndex, ref int sum, ref int maxNumIndex)
        {
            // skip spaces;
            while (index < input.Length && input[index] == '.')
                index++;

            // finished
            if (index >= input.Length)
                return;

            if (input[index] == '?')
                Recurse(input, index + 1, nums, numIndex, ref sum, ref maxNumIndex);

            var num = nums[numIndex];

            // bad, outside
            if (num + index >  input.Length)
                return;

            for (int i = 0; i < num; i++)
            {
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
            Recurse(input, index + 1, nums, numIndex + 1, ref sum, ref maxNumIndex);
        }
    }
}
