using AdventOfCode.Helpers;
using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay10 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day10(InputProvider);
            day.Pipe = '7';
            day.Dir = Coords2D.Right;

            Assert.Equal(80, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day10(InputProvider);
            day.Pipe = '7';
            day.Dir = Coords2D.Right;
            Assert.Equal(10, day.Part2());
        }

        protected override string[] AocInput()
        {
            return [
                "FF7FSF7F7F7F7F7F---7",
                "L|LJ||||||||||||F--J",
                "FL-7LJLJ||||||LJL-77",
                "F--JF--7||LJLJ7F7FJ-",
                "L---JF-JLJ.||-FJLJJ7",
                "|F|F-JF---7F7-L7L|7|",
                "|FFJF7L7F-JF7|JL---7",
                "7-L-JL7||F7|L7F-7F7|",
                "L.L7LFJ|||||FJL7||LJ",
                "L7JLJL-JLJLJL--JLJ.L",
            ];
        }

    }
}
