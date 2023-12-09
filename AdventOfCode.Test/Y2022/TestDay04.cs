using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay04 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day04(InputProvider);
            Assert.Equal(2, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day04(InputProvider);
            Assert.Equal(4, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "2-4,6-8",
                "2-3,4-5",
                "5-7,7-9",
                "2-8,3-7",
                "6-6,4-6",
                "2-6,4-8",
            };
        }
    }
}
