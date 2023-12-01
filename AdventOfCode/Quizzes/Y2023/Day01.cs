using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day01 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day01(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();
            return FindCalibationValue(input);
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();

            var wordToNumber = new (string word, string number)[]
            {
                ("oneight", "18"),
                ("twone", "21"),
                ("threeight", "38"),
                ("fiveight", "58"),
                ("sevenine", "79"),
                ("eightwo", "82"),
                ("eighthree", "83"),
                ("nineight", "98"),
                ("one", "1"),
                ("two", "2"),
                ("three", "3"),
                ("four", "4"),
                ("five", "5"),
                ("six", "6"),
                ("seven", "7"),
                ("eight", "8"),
                ("nine", "9"),
            };

            var replaced = input.Select(line => wordToNumber.Aggregate(line, (current, tuple) => current.Replace(tuple.word, tuple.number)));
            return FindCalibationValue(replaced);
        }

        private static long FindCalibationValue(IEnumerable<string> input)
        {
            var result = input.Select(x => x.Nums()
                  .Select(y => y.ToString()))
                  .Select(innerList => (innerList.First()[0], innerList.Last()[^1]))
                  .Select(x => int.Parse($"{x.Item1}{x.Item2}"))
                  .Sum();
            return result;
        }
    }
}
