using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day03 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day03(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();
            var map = new Dictionary<(int, int), IList<int>>();

            var value = input.Select((line, lineIndex) => Regex.Matches(line, @"\d+").Select(m => CheckMatch(m, lineIndex, input, map)).Sum()).Sum();

            return value;
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();

            var map = Collect(input);

            return map.Where(pair => input[pair.Key.Item1][pair.Key.Item2] == '*' && pair.Value.Count == 2)
                      .Select(kv => kv.Value[0] * kv.Value[1])
                      .Sum();
        }

        static Dictionary<(int, int), IList<int>> Collect(IList<string> input)
        {
            var map = new Dictionary<(int, int), IList<int>>();

            input.ForEach((line, lineIndex) => Regex.Matches(line, @"\d+").ForEach(m => CheckMatch(m, lineIndex, input, map)));

            return map;
        }

        static int CheckMatch(Match match, int lineNumber, IList<string> input, Dictionary<(int, int), IList<int>> map)
        {
            var flag = false;

            for (int i = lineNumber - 1; i <= lineNumber + 1; i++)
            {
                for (int j = match.Index - 1; j < match.Index + match.Value.Length + 1; j++)
                {
                    if (!Bounds.Within(input[lineNumber].Length, input.Count, i, j))
                        continue;

                    if (char.IsDigit(input[i][j]) || input[i][j] == '.')
                        continue;

                    if (!map.ContainsKey((i, j)))
                        map[(i, j)] = new List<int>();

                    map[(i, j)].Add(int.Parse(match.Value));
                    flag = true;
                }
            }

            return flag ? int.Parse(match.Value) : 0;
        }
    }
}
