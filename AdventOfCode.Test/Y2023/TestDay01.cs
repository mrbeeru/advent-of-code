using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay01 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day01(InputProvider);
            Assert.Equal(209, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day01(InputProvider);
            Assert.Equal(278, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[]
            {
                "two1nine",
                "eightwothree0",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen",
            };
        }
    }
}
