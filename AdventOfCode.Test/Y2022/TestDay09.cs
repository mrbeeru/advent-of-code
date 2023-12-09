using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay09 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day09(InputProvider);
            Assert.Equal(88, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day09(InputProvider);
            Assert.Equal(36, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "R 5",
                "U 8",
                "L 8",
                "D 3",
                "R 17",
                "D 10",
                "L 25",
                "U 20",
            };
        }
    }
}
