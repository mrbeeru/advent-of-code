using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay14 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day14(InputProvider);

            Assert.Equal(136, day.Part1());
        }

        protected override string[] AocInput()
        {
            return [
                "O....#....",
                "O.OO#....#",
                ".....##...",
                "OO.#O....O",
                ".O.....O#.",
                "O.#..O.#.#",
                "..O..#O..O",
                ".......O..",
                "#....###..",
                "#OO..#....",
            ];

        }

    }
}
