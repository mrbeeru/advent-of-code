using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay13 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day13(InputProvider);

            Assert.Equal(405, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day13(InputProvider);
            Assert.Equal(400, day.Part2());
        }

        protected override string[] AocInput()
        {
            return [
                "#.##..##.",
                "..#.##.#.",
                "##......#",
                "##......#",
                "..#.##.#.",
                "..##..##.",
                "#.#.##.#.",
                "",
                "#...##..#",
                "#....#..#",
                "..##..###",
                "#####.##.",
                "#####.##.",
                "..##..###",
                "#....#..#",
            ];

        }

    }
}
