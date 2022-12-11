using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay03 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day03(InputProvider);
            Assert.Equal(157, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day03(InputProvider);
            Assert.Equal(70, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "vJrwpWtwJgWrhcsFMMfFFhFp",
                "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
                "PmmdzqPrVvPwwTWBwg",
                "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
                "ttgJtRGJQctTZtZT",
                "CrZsJsPPZsGzwwsLwLmpwMDw",
            };
        }
    }
}
