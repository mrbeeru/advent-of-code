using AdventOfCode.Extensions;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 1)]
    public class Day01(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput();
            return FindCalibationValue(input);
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();

            // worked on my input when I tried, but this fails in certain cases e.g 'twoneight'
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
            var result = input
                .Select(line => line.Nums().Select(num => num.ToString()))
                .Select(numStr => int.Parse($"{numStr.First()[0]}{numStr.Last()[^1]}"))
                .Sum();
            return result;
        }
    }
}
