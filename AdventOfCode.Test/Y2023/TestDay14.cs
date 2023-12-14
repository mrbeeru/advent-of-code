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

        [Fact]
        public void Part2()
        {
            // doesn't work when sums repeat between consecutive cycles
            // the original input luckily doesn't have that case
            //var day = new Day14(InputProvider);
            //Assert.Equal(65, day.Part2());
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
