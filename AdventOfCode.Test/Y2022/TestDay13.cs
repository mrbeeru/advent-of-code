using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay13 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day13(InputProvider);
            Assert.Equal(13, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day13(InputProvider);
            Assert.Equal(140, a.Part2());
        }


        protected override string[] AocInput()
        {
            return new[] {
                "[1,1,3,1,1]",
                "[1,1,5,1,1]",
                "",
                "[[1],[2,3,4]]",
                "[[1],4]",
                "",
                "[9]",
                "[[8,7,6]]",
                "",
                "[[4,4],4,4]",
                "[[4,4],4,4,4]",
                "",
                "[7,7,7,7]",
                "[7,7,7]",
                "",
                "[]",
                "[3]",
                "",
                "[[[]]]",
                "[[]]",
                "",
                "[1,[2,[3,[4,[5,6,7]]]],8,9]",
                "[1,[2,[3,[4,[5,6,0]]]],8,9]"
            };
        }
    }
}
