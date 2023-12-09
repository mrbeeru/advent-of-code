using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay04 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day04(InputProvider);
            Assert.Equal(13, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day04(InputProvider);
            Assert.Equal(30, day.Part2());
        }


        protected override string[] AocInput()
        {
            return new string[] {
                "Card 1: 41 48 83 86 17 0 0 0 0 0 | 83 86  6 31 17  9 48 53",
                "Card 2: 13 32 20 16 61 0 0 0 0 0 | 61 30 68 82 17 32 24 19",
                "Card 3:  1 21 53 59 44 0 0 0 0 0 | 69 82 63 72 16 21 14  1",
                "Card 4: 41 92 73 84 69 0 0 0 0 0 | 59 84 76 51 58  5 54 83",
                "Card 5: 87 83 26 28 32 0 0 0 0 0 | 88 30 70 12 93 22 82 36",
                "Card 6: 31 18 13 56 72 0 0 0 0 0 | 74 77 10 23 35 67 36 11",
            };
        }
    }
}
