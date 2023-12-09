using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay24 : TestBase
    {
        [Fact]
        public void Part1() => Assert.Equal(18, new Day24(InputProvider).Part1());

        [Fact]
        public void Part2() => Assert.Equal(54, new Day24(InputProvider).Part2());

        protected override string[] AocInput()
        {
            return new[]
            {
                "#.######",
                "#>>.<^<#",
                "#.<..<<#",
                "#>v.><>#",
                "#<^v^^>#",
                "######.#",
            };
        }
    }
}
