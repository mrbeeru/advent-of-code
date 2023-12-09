using AdventOfCode.Reader;
using static MoreLinq.Extensions.WindowExtension;

namespace AdventOfCode.Quizzes.Y2015
{
    [Aoc(year: 2015, day: 5)]
    public class Day05(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput();
            var rules = new[] { VowelsRule, TwiceRule, ForbiddenRule };

            return input.Where(str => rules.All(rule => rule.Invoke(str))).Count();
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();
            var rules = new[] { RepeatsRule, PairNoOverlapRule };

            return input.Where(str => rules.All(rule => rule.Invoke(str))).Count();
        }

        private bool VowelsRule(string input)
        {
            List<char> vowles = new() { 'a', 'e', 'i', 'o', 'u' };
            return input.Count(vowles.Contains) >= 3;
        }

        private bool TwiceRule(string input)
        {
            return input.Window(2).Any(x => x[0] == x[1]);
        }

        private bool ForbiddenRule(string input)
        {
            List<string> forbidden = new() { "ab", "cd", "pq", "xy" };
            return forbidden.All(x => input.Contains(x) == false);
        }

        private bool RepeatsRule(string input)
        {
            return input.Window(3).Any(x => x[0] == x[2]);
        }

        private bool PairNoOverlapRule(string input)
        {
            return input
                .Window(2)
                .Where((x, i) => input.Substring(i + 2).Contains($"{x[0]}{x[1]}"))
                .Any();
        }
    }
}
