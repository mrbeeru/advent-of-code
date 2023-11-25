using AdventOfCode.Quizzes.Y2015;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2015
{
    public class TestDay08 : TestBase
    {
        [Fact]
        public void Part1()
        {
            Assert.Equal(12, new Day08(InputProvider).Part1());
        }

        //[Fact]
        //public void Part2()
        //{
        //    Assert.Equal(24, new Day08(InputProvider).Part2());
        //}

        protected override string[] AocInput()
        {
            return new string[]
            {
                "",
                "abc",
                "aaa\"aaa",
                "\x27"
            };
        }
    }
}
