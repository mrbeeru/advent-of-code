using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using MoreLinq;
using System.Text.RegularExpressions;

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
            var map = BuildSymbolToAdjacentValuesMap(input);

            return map.Where(pair => pair.Value.Count > 0).Sum(pair => pair.Value.Sum());
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();
            var map = BuildSymbolToAdjacentValuesMap(input);

            return map.Where(pair => input[pair.Key.Item1][pair.Key.Item2] == '*' && pair.Value.Count == 2)
                      .Select(kv => kv.Value[0] * kv.Value[1])
                      .Sum();
        }

        static Dictionary<(int, int), IList<int>> BuildSymbolToAdjacentValuesMap(IList<string> input)
        {
            var map = new Dictionary<(int, int), IList<int>>();
            var matrix = input.Select(x => x.ToArray()).ToArray();

            input.ForEach((line, lineIndex) => Regex.Matches(line, @"\d+").ForEach(m => CheckMatch(m, lineIndex, matrix, map)));

            return map;
        }

        static void CheckMatch(Match match, int lineNumber, char[][] matrix, Dictionary<(int, int), IList<int>> map)
        {
            for (int i = lineNumber - 1; i <= lineNumber + 1; i++)
            {
                for (int j = match.Index - 1; j < match.Index + match.Value.Length + 1; j++)
                {
                    if (!(i, j).Within(matrix))
                        continue;

                    if (char.IsDigit(matrix[i][j]) || matrix[i][j] == '.')
                        continue;

                    if (!map.ContainsKey((i, j)))
                        map[(i, j)] = new List<int>();

                    map[(i, j)].Add(int.Parse(match.Value));
                }
            }
        }
    }
}
