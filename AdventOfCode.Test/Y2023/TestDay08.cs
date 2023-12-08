using AdventOfCode.Quizzes.Y2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuikGraph.Algorithms.Assignment.HungarianAlgorithm;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay08 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day08(InputProvider);
            Assert.Equal(2, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day08(InputProvider);
            Assert.Equal(6, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "LR",
                "",
                "11A = (11B, XXX)",
                "11B = (XXX, 11Z)",
                "11Z = (11B, XXX)",
                "22A = (22B, XXX)",
                "22B = (22C, 22C)",
                "22C = (22Z, 22Z)",
                "22Z = (22B, 22B)",
                "XXX = (XXX, XXX)",
            };
        }
    }
}
