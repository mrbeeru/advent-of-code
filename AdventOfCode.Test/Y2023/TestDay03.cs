using AdventOfCode.Quizzes.Y2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2023
{
    public class TestDay03 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var day = new Day03(InputProvider);
            Assert.Equal(4361, day.Part1());
        }

        [Fact]
        public void Part2()
        {
            var day = new Day03(InputProvider);
            Assert.Equal(467835, day.Part2());
        }


        protected override string[] AocInput()
        {
            return new string[] {
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598..",
            };
        }
    }
}
