using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay05 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day05(InputProvider);
            Assert.Equal("CMZ", a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day05(InputProvider);
            Assert.Equal("MCD", a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "    [D]    ",
                "[N] [C]    ",
                "[Z] [M] [P]",
                " 1   2   3 ",
                "",
                "move 1 from 2 to 1",
                "move 3 from 1 to 3",
                "move 2 from 2 to 1",
                "move 1 from 1 to 2",
            };
        }
    }
}
