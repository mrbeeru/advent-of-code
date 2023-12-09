using AdventOfCode.Reader;
using System.Text.RegularExpressions;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 2)]
    public class Day02(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput();

            var result = input.Select(MaxGameBallsByColor)
                .Select((map, i) => (map, i))
                .Where(x => x.map["red"] <= 12 && x.map["green"] <= 13 && x.map["blue"] <= 14)
                .Sum(x => x.i + 1);

            return result;
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();

            var result = input.Select(MaxGameBallsByColor)
                .Select(map => map["red"] * map["green"] * map["blue"])
                .Sum();

            return result;
        }

        static Dictionary<string, int> MaxGameBallsByColor(string game)
        {
            var map = new Dictionary<string, int> { { "red", 1 }, { "green", 1 }, { "blue", 1 } };
            var matches = Regex.Matches(game, @"(\d+) (red|green|blue)");

            foreach (var match in matches.Cast<Match>())
            {
                var color = match.Groups[2].Value;
                var count = int.Parse(match.Groups[1].Value);
                map[color] = Math.Max(map[color], count);
            }

            return map;
        }
    }
}
