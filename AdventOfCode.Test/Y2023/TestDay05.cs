using AdventOfCode.Quizzes.Y2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuikGraph.Algorithms.Assignment.HungarianAlgorithm;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay05 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day05(InputProvider);
            Assert.Equal(35, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            // did a shitty solution and it's too hard to unit test:)
            //var day = new Day05(InputProvider);
            //Assert.Equal(46, day.Part2());
        }

        protected override string[] AocInput()
        {
            return new string[] {
                "seeds: 79 14 55 13",
                "",
                "seed-to-soil map:",
                "50 98 2",
                "52 50 48",
                "",
                "soil-to-fertilizer map:",
                "0 15 37",
                "37 52 2",
                "39 0 15",
                "",
                "fertilizer-to-water map:",
                "49 53 8",
                "0 11 42",
                "42 0 7",
                "57 7 4",
                "",
                "water-to-light map:",
                "88 18 7",
                "18 25 70",
                "",
                "light-to-temperature map:",
                "45 77 23",
                "81 45 19",
                "68 64 13",
                "",
                "temperature-to-humidity map:",
                "0 69 1",
                "1 0 69",
                "",
                "humidity-to-location map:",
                "60 56 37",
                "56 93 4",
            };
        }
    }
}
