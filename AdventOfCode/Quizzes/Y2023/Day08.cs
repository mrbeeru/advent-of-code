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

            for (int i = 0; ; i++)
            {
                current = Next(i, current, directions, map);

                if (current == "ZZZ")
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
                for (int i = 0, zpos = 0; ; i++)
                {
                    current[j] = Next(i, current[j], directions, map);

                    if (current[j].EndsWith('Z') )
                    {
                        if (zpos > 0)
                        {
                            cycles.Add(i - zpos);
                            break;
                        }

                        zpos = i;
                    }
                }
            }

            return cycles.Aggregate(1L, (result, next) => MathUtils.LCM(result, next));
        }

        static string Next(int i, string current, char[] directions, Dictionary<string, (string Left, string Right)> map)
        {
            return directions[i % directions.Length] == 'L' ? map[current].Left : map[current].Right;
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
