using AdventOfCode.Quizzes.Y2015;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay01 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day01(InputProvider);
            Assert.Equal(1, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day01(InputProvider);
            Assert.Equal(7, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] { "((())))((" }; 
        }
    }
}
