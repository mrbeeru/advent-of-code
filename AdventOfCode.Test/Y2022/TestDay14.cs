using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay14 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day14(InputProvider);
            Assert.Equal(24, a.Part1());
        }

        protected override string[] AocInput()
        {
            return new[]
            {
                "498,4 -> 498,6 -> 496,6", 
                "503,4 -> 502,4 -> 502,9 -> 494,9"
            };
        }
    }
}
