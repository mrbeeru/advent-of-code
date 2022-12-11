using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Quizzes.Y2022;
using AdventOfCode.Reader;
using Moq;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay01 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day01(InputProvider);
            Assert.Equal(24000, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day01(InputProvider);
            Assert.Equal(45000, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[]{
            "1000",
            "2000",
            "3000",
            ""    ,
            "4000",
            ""    ,
            "5000",
            "6000",
            ""    ,
            "7000",
            "8000",
            "9000",
            ""    ,
            "10000"};
        }
    }
}
