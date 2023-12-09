using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay08 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day08(InputProvider);
            Assert.Equal(21, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day08(InputProvider);
            Assert.Equal(8, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "30373",
                "25512",
                "65332",
                "33549",
                "35390",
            };
        }
    }
}
