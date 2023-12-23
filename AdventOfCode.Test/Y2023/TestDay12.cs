using AdventOfCode.Quizzes.Y2023;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay12 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day12(InputProvider);
            Assert.Equal(405, day.Part1());
        }

        protected override string[] AocInput()
        {
            return [
                "???.### 1,1,3",
                ".??..??...?##. 1,1,3",
                "?#?#?#?#?#?#?#? 1,3,1,6",
                "????.#...#... 4,1,1",
                "????.######..#####. 1,6,5",
                "?###???????? 3,2,1",
            ];
        }
    }
}
