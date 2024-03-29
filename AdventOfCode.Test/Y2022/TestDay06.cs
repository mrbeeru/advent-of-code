﻿using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay06 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day06(InputProvider);
            Assert.Equal(10, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day06(InputProvider);
            Assert.Equal(29, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg" };
        }
    }
}
