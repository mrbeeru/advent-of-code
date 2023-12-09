using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay08 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day08(InputProvider);
            Assert.Equal(2, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day08(InputProvider);
            Assert.Equal(6, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "LR",
                "",
                "AAA = (BBB, BBB)",
                "BBB = (AAA, ZZZ)",
                "ZZZ = (ZZZ, ZZZ)",
                "11A = (11B, XXX)",
                "11B = (XXX, 11Z)",
                "11Z = (11B, XXX)",
                "22A = (22B, XXX)",
                "22B = (22C, 22C)",
                "22C = (22Z, 22Z)",
                "22Z = (22B, 22B)",
                "XXX = (XXX, XXX)",
            };
        }
    }
}
