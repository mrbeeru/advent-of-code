using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay11 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day11(InputProvider);

            Assert.Equal(374, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day11(InputProvider);

            Assert.Equal(8410, day.Part2());
        }

        protected override string[] AocInput()
        {
            return [
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#.....",
            ];
        }

    }
}
