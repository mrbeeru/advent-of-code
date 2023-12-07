using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day07 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day07(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1() => CalculateTotalScore(1);

        public long Part2() => CalculateTotalScore(2);

        private long CalculateTotalScore(int part)
        {
            var ordered = inputProvider.GetInput()
                .Select(x => (x[..5], int.Parse(x[6..])))
                .OrderBy(x => CalculateHandStrength(x.Item1, part));

            return ordered.Select((x, i) => x.Item2 * (i + 1)).Sum();
        }

        static long CalculateHandStrength(string hand, int part)
        {
            var map = GetCardMapping(part);
            var cardGroups = hand.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var nonJcards = cardGroups.Where(x => x.Key != 'J').Select(x => x.Value).ToList();
            
            if (nonJcards.Any()) 
                nonJcards[nonJcards.IndexOf(nonJcards.Max())] += cardGroups.GetValueOrDefault('J', 0);

            var handLevel = CalcHandLevel(part == 2 ? nonJcards : cardGroups.Values);
            return handLevel * 11390625L + hand.Select((x, i) => map[x] * (long)Math.Pow(15, 5-i)).Sum();
        }

        static long CalcHandLevel(IEnumerable<int> frequencies)
        {
            return frequencies switch
            {
                var x when !x.Any() => 7, //case when only JJJJJ in part 2
                var x when x.Contains(5) => 7,
                var x when x.Contains(4) => 6,
                var x when x.Contains(3) && x.Contains(2) => 5,
                var x when x.Contains(3) => 4,
                var x when x.Count(y => y == 2) == 2 => 3,
                var x when x.Max() == 2 => 2,
                _ => 1,
            };
        }

        static Dictionary<char, int> GetCardMapping(int part)
        {
            return "AKQJT98765432".Select((x, i) => (x, x == 'J' && part == 2 ? 1 : 14 - i)).ToDictionary(x => x.x, x => x.Item2);
        }
    }
}
