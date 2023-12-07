using AdventOfCode.Quizzes.Y2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuikGraph.Algorithms.Assignment.HungarianAlgorithm;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay07 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day07(InputProvider);
            Assert.Equal(6440, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day07(InputProvider);
            Assert.Equal(5905, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483",
            };
        }
    }
}
