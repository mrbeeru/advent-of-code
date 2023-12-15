using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay15 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day15(InputProvider);

            Assert.Equal(1320, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day15(InputProvider);

            Assert.Equal(145, day.Part2());
        }

        protected override string[] AocInput()
        {
            return ["rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"];
        }
    }
}
