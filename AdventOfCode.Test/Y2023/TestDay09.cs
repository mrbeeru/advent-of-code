using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay09 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day09(InputProvider);
            Assert.Equal(114, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day09(InputProvider);
            Assert.Equal(2, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "0 3 6 9 12 15",
                "1 3 6 10 15 21",
                "10 13 16 21 30 45",
            };
        }

    }
}
