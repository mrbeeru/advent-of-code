using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay02 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day02(InputProvider);
            Assert.Equal(8, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day02(InputProvider);
            Assert.Equal(2286, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
            };
        }
    }
}
