﻿using AdventOfCode.Quizzes.Y2022;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay23 : TestBase
    {
        [Fact]
        public void Part1() => Assert.Equal(110, new Day23(InputProvider).Part1());

        [Fact]
        public void Part2() => Assert.Equal(20, new Day23(InputProvider).Part2());

        protected override string[] AocInput()
        {
            return new[]
            {
                "..............",
                "..............",
                ".......#......",
                ".....###.#....",
                "...#...#.#....",
                "....#...##....",
                "...#.###......",
                "...##.#.##....",
                "....#..#......",
                "..............",
                "..............",
                "..............",
            };
        }
    }
}
