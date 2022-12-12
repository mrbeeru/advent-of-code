using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay12 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day12(InputProvider);
            Assert.Equal(31, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day12(InputProvider);
            Assert.Equal(29, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[]
            {
                "Sabqponm",
                "abcryxxl",
                "accszExk",
                "acctuvwj",
                "abdefghi",
            };
        }
    }
}
