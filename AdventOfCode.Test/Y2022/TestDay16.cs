using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay16 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day16(InputProvider);
            Assert.Equal(1651, a.Part1());
        }

        protected override string[] AocInput()
        {
            return new[]
            {
                "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
                "Valve BB has flow rate = 13; tunnels lead to valves CC, AA",
                "Valve CC has flow rate = 2; tunnels lead to valves DD, BB",
                "Valve DD has flow rate = 20; tunnels lead to valves CC, AA, EE",
                "Valve EE has flow rate = 3; tunnels lead to valves FF, DD",
                "Valve FF has flow rate = 0; tunnels lead to valves EE, GG",
                "Valve GG has flow rate = 0; tunnels lead to valves FF, HH",
                "Valve HH has flow rate = 22; tunnel leads to valve GG",
                "Valve II has flow rate = 0; tunnels lead to valves AA, JJ",
                "Valve JJ has flow rate = 21; tunnel leads to valve II",
            };
        }
    }
}
