using AdventOfCode.Quizzes.Y2015;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay03 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day03(InputProvider);
            Assert.Equal(2, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day03(InputProvider);
            Assert.Equal(11, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] { "^v^v^v^v^v" };
        }
    }
}
