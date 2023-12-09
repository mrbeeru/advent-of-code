using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay14 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day14(InputProvider);
            Assert.Equal(24, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day14(InputProvider);
            Assert.Equal(93, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[]
            {
                "498,4 -> 498,6 -> 496,6",
                "503,4 -> 502,4 -> 502,9 -> 494,9"
            };
        }
    }
}
