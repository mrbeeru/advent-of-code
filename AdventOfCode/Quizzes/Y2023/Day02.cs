using AdventOfCode.Reader;
using MoreLinq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day02 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day02(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

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
            var matches = Regex.Matches(game, @"(\d+) (red|green|blue)");

            //an array like: [("red", 3), ("blue", 1)]
            var balls = matches.Select(x => new { Color = x.Groups[2].Value, Count = int.Parse(x.Groups[1].Value) });

            var map = new Dictionary<string, int> { { "red", 1 }, { "green", 1 }, { "blue", 1 } };
            balls.ForEach((b) => map[b.Color] = Math.Max(map[b.Color], b.Count));

            return map;
        }
    }
}
