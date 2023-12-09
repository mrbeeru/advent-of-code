using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay11 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day11(InputProvider);
            Assert.Equal(10605, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day11(InputProvider);


            Assert.Equal(2713310158, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "Monkey 0:",
                "  Starting items: 79, 98",
                "  Operation: new = old * 19",
                "  Test: divisible by 23",
                "    If true: throw to monkey 2",
                "    If false: throw to monkey 3",
                "",
                "Monkey 1:",
                "  Starting items: 54, 65, 75, 74",
                "  Operation: new = old + 6",
                "  Test: divisible by 19",
                "    If true: throw to monkey 2",
                "    If false: throw to monkey 0",
                "",
                "Monkey 2:",
                "  Starting items: 79, 60, 97",
                "  Operation: new = old * old",
                "  Test: divisible by 13",
                "    If true: throw to monkey 1",
                "    If false: throw to monkey 3",
                "",
                "Monkey 3:",
                "  Starting items: 74",
                "  Operation: new = old + 3",
                "  Test: divisible by 17",
                "    If true: throw to monkey 0",
                "    If false: throw to monkey 1",
            };
        }
    }
}
