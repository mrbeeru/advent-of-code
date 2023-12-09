using AdventOfCode.Quizzes.Y2015;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay05 : TestBase
    {
        [Fact]
        public void Part1()
        {
            Assert.Equal(2, new Day05(InputProvider).Part1());
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(2, new Day05(InputProvider).Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "ugknbfddgicrmopn",
                "aaa",
                "jchzalrnumimnmhp",
                "haegwjzuvuyypxyu",
                "dvszwmarrgswjxmb",
                "qjhvhtzxzqqjkmpb",
                "xxyxx",
                "uurcxstgmygtbstg",
                "ieodomkazucvgmuy",
            };
        }
    }
}
