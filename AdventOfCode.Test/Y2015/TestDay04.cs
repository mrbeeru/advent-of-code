using AdventOfCode.Quizzes.Y2015;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay04 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day04(InputProvider);
            Assert.Equal(1048970, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day04(InputProvider);
            Assert.Equal(5714438, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] { "pqrstuv" };
        }
    }
}
