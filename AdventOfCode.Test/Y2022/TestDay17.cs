using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay17 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day17(InputProvider);
            Assert.Equal(3068, a.Part1());
        }

        protected override string[] AocInput()
        {
            return new[] { ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>" };
        }
    }
}
