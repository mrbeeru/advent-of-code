using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay02 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day02(InputProvider);
            Assert.Equal(15, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day02(InputProvider);
            Assert.Equal(12, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] { "A Y", "B X", "C Z" };
        }
    }
}
