using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/2
    /// </summary>
    internal class Day02 : IAocDay<long>
    {
        private readonly IInputProvider inputProvider;

        public Day02(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = ParseInput();
            return input.Select(x => GetScorePart1(x)).Sum();
        }

        public long Part2()
        {
            var input = ParseInput();
            return input.Select(x => GetScorePart2(x)).Sum();
        }

        private int GetScorePart1(string round)
        {
            return round switch
            {
                "A X" => 1 + 3,
                "A Y" => 2 + 6, 
                "A Z" => 3 + 0,
                "B X" => 1 + 0,
                "B Y" => 2 + 3,
                "B Z" => 3 + 6,
                "C X" => 1 + 6,
                "C Y" => 2 + 0,
                "C Z" => 3 + 3,
                _ => throw new Exception("Invalid input.")
            };
        }

        private int GetScorePart2(string round)
        {
            return round switch
            {
                "A X" => 3 + 0,
                "A Y" => 1 + 3,
                "A Z" => 2 + 6,
                "B X" => 1 + 0,
                "B Y" => 2 + 3,
                "B Z" => 3 + 6,
                "C X" => 2 + 0,
                "C Y" => 3 + 3,
                "C Z" => 1 + 6,
                _ => throw new Exception("Invalid input.")
            };
        }

        private IEnumerable<string> ParseInput()
        {
            var input = inputProvider.GetInput();
            return Regex.Split(input, "\r\n|\r|\n");
        }
    }
}
