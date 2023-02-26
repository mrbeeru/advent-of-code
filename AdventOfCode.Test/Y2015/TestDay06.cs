using AdventOfCode.Quizzes.Y2015;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay06 : TestBase
    {
        [Fact]
        public void Part1()
        {
            Assert.Equal(12, new Day06(InputProvider).Part1());
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(24, new Day06(InputProvider).Part2());
        }

        protected override string[] AocInput()
        {
            return new string[]
            {
                "toggle 0,0 through 2,2", 
                "turn off 1,1 through 2,2", 
                "turn on 0,0 through 0,9"
            };
        }
    }
}
