using AdventOfCode.Quizzes.Y2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuikGraph.Algorithms.Assignment.HungarianAlgorithm;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay06 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day06(InputProvider);
            Assert.Equal(288, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day06(InputProvider);
            Assert.Equal(71503, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "Time:      7  15   30",
                "Distance:  9  40  200",
            };
        }
    }
}
