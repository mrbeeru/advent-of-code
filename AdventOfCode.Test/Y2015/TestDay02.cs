using AdventOfCode.Quizzes.Y2015;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay02 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day02(InputProvider);
            Assert.Equal(58 + 43, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day02(InputProvider);
            Assert.Equal(34 + 14, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] { "2x3x4", "1x1x10" };
        }
    }
}
