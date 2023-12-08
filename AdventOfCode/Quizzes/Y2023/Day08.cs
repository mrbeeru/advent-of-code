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
    public class Day08 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day08(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var (map, directions) = ParseInput();
            var current = "AAA";
            var target = "ZZZ";

            for (int i = 0; ; i++)
            {
                var direction = directions[i % directions.Length];
                current = direction == 'L' ? map[current].Left : map[current].Right;

                if (current == target)
                    return i + 1;
            }
        }

        public long Part2()
        {
            var (map, directions) = ParseInput();
            var current = map.Where(x => x.Key.EndsWith('A')).Select(x => x.Key).ToArray();

            var cycles = new List<int>();

            for (int j = 0; j < current.Length; j++)
            {
                bool seenZ = false;
                for (int i = 0, k = 0; ; i++)
                {
                    var direction = directions[i % directions.Length];
                    current[j] = direction == 'L' ? map[current[j]].Left : map[current[j]].Right;

                    if (current[j].EndsWith('Z') && seenZ == true)
                    {
                        cycles.Add(i - k);
                        break;
                    }

                    if (current[j].EndsWith('Z') )
                    {
                        seenZ = true;
                        k = i;
                    }
                }
            }

            return cycles.Aggregate(1L, (result, next) => MathUtils.LCM(result, next));
        }

        (Dictionary<string, (string Left, string Right)>, char[]) ParseInput()
        {
            var input = inputProvider.GetInput();
            var directions = input.First().ToArray();
            var map = input.Skip(2)
                .Select(x => (x[0..3], x[7..10], x[12..15]))
                .ToDictionary(x => x.Item1, x => (x.Item2, x.Item3));

            return (map, directions);
        }
    }
}
