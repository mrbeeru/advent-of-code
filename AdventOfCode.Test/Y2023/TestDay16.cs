using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay16 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day16(InputProvider);

            Assert.Equal(46, day.Part1());
        }

        protected override string[] AocInput()
        {
            return [
                @".|...\....",
                @"|.-.\.....",
                @".....|-...",
                @"........|.",
                @"..........",
                @".........\",
                @"..../.\\..",
                @".-.-/..|..",
                @".|....-|.\",
                @"..//.|....",
            ];
        }
    }
}
