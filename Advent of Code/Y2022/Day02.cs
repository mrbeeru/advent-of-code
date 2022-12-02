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

        private const int WIN = 6;
        private const int DRAW = 3;
        private const int LOSE = 0;

        private const int PICK_ROCK = 1;
        private const int PICK_PAPER = 2;
        private const int PICK_SCISSORS = 3;


        public Day02(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = ParseInput();
            return input.Select(x => Evaluate(Decrypt1(x))).Sum();
        }

        public long Part2()
        {
            var input = ParseInput();
            return input.Select(x => Evaluate(Decrypt2(x))).Sum();
        }

        private string Decrypt1(string round)
        {
            return round[0] + " " + (char)(round[2] - ('X' - 'A'));
        }

        private string Decrypt2(string round)
        {
            return round[0] + " " + (char)('A' + (round[0] - 'A' + round[2] - 'X' + 2) % 3);
        }

        private int Evaluate(string round)
        {
            return round switch
            {
                "A A" => PICK_ROCK      + DRAW,
                "A B" => PICK_PAPER     + WIN,
                "A C" => PICK_SCISSORS  + LOSE,
                "B A" => PICK_ROCK      + LOSE,
                "B B" => PICK_PAPER     + DRAW,
                "B C" => PICK_SCISSORS  + WIN,
                "C A" => PICK_ROCK      + WIN,
                "C B" => PICK_PAPER     + LOSE,
                "C C" => PICK_SCISSORS  + DRAW,
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
