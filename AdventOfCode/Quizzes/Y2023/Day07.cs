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

        public long Part1()
        {
            return CalculateTotalScore(CalculateHandStrengthPart1);
        }

        public long Part2()
        {
            return CalculateTotalScore(CalculateHandStrength2);

        }

        private long CalculateTotalScore(Func<string, long> calculateHandStrength)
        {
            var ordered = inputProvider.GetInput()
                .Select(x => (x[..5], int.Parse(x[6..])))
                .OrderBy(x => calculateHandStrength(x.Item1));

            return ordered.Select((x, i) => x.Item2 * (i + 1)).Sum();
        }

        static long CalculateHandStrengthPart1(string hand)
        {
            var map = GetCardMapping(11);
            var cards = hand.GroupBy(x => x).Select(x => x.Count());
            var handValue = cards switch
            {
                var x when x.Contains(5) => 7,
                var x when x.Contains(4) => 6,
                var x when x.Contains(3) && x.Contains(2) => 5,
                var x when x.Contains(3) => 4,
                var x when x.Count(y => y == 2) == 2 => 3,
                var x when x.Max() == 2 => 2,
                _ => 1,
            };

            var power = handValue * 11390625L + hand.Select((x, i) => map[x] * (long)Math.Pow(15, 5-i)).Sum();

            return power;
        }

        static long CalculateHandStrength2(string hand)
        {
            var map = GetCardMapping(1);
            var cardGroups = hand.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var nonJcards = cardGroups.Where(x => x.Key != 'J').ToDictionary(x => x.Key, x => x.Value);
            var jcount = cardGroups.GetValueOrDefault('J', 0);

            if ( nonJcards.Count > 0)
            {
                var key = nonJcards.MaxBy(x => x.Value).Key;
                nonJcards[key] += jcount;
            }

            var handValue = nonJcards.Values switch
            {
                var x when !nonJcards.Any() => 7,
                var x when x.Contains(5) => 7,
                var x when x.Contains(4) => 6,
                var x when x.Contains(3) && x.Contains(2) => 5,
                var x when x.Contains(3) => 4,
                var x when x.Count(y => y == 2) == 2 => 3,
                var x when x.Max() == 2 => 2,
                _ => 1,
            };

            var power = handValue * 11390625L + hand.Select((x, i) => map[x] * (long)Math.Pow(15, 5-i)).Sum();

            return power;
        }

        static Dictionary<char, int> GetCardMapping(int jval)
        {
            return new Dictionary<char, int>() {
                { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J', jval }, { 'T', 10 }, { '9', 9 },
                { '8',  8 }, { '7',  7 }, { '6',  6 }, { '5', 5 }, { '4',  4 }, { '3', 3 }, { '2', 2 }
            };
        }
    }
}
