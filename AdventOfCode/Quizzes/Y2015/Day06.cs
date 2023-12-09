using AdventOfCode.Extensions;
using AdventOfCode.Helpers;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2015
{
    [Aoc(year: 2015, day: 6)]
    public class Day06(IInputProvider inputProvider) : IPartOne<int>, IPartTwo<int>
    {
        private int[,] grid = new int[1000, 1000];

        public int Part1()
        {
            Func<int, int> toggle = (light) => light == 1 ? 0 : 1;
            Func<int, int> turnOn = (light) => 1;
            Func<int, int> turnOff = (light) => 0;

            Execute(toggle, turnOn, turnOff);

            return CountLights();
        }
        public int Part2()
        {
            Func<int, int> toggle = (light) => light + 2;
            Func<int, int> turnOn = (light) => light + 1;
            Func<int, int> turnOff = (light) => light > 0 ? light - 1 : 0;

            Execute(toggle, turnOn, turnOff);

            return CountLights();
        }

        private void Execute(Func<int, int> toggle, Func<int, int> turnOn, Func<int, int> turnOff)
        {
            var input = inputProvider.GetInput();

            foreach (var command in input)
            {
                var nums = command.Nums().ToArray();
                var a = new Coords2D(Math.Min(nums[0], nums[2]), Math.Min(nums[1], nums[3]));
                var b = new Coords2D(Math.Max(nums[0], nums[2]), Math.Max(nums[1], nums[3]));

                if (command.StartsWith("toggle"))
                    ExecuteCommand(a, b, toggle);
                if (command.StartsWith("turn on"))
                    ExecuteCommand(a, b, turnOn);
                if (command.StartsWith("turn off"))
                    ExecuteCommand(a, b, turnOff);
            }
        }

        private void ExecuteCommand(Coords2D a, Coords2D b, Func<int, int> command)
        {
            for (int i = a.X; i <= b.X; i++)
            {
                for (int j = a.Y; j <= b.Y; j++)
                {
                    grid[i, j] = command(grid[i, j]);
                }
            }
        }

        private int CountLights()
        {
            int count = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    count += grid[i, j];
                }
            }

            return count;
        }
    }
}
