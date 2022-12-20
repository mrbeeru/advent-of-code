using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay20 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day20(InputProvider);
            Assert.Equal(3, a.Part1());
        }

        protected override string[] AocInput()
        {
            return new[]
            {
                "1",
                "2",
                "-3",
                "3",
                "-2",
                "0",
                "4",
            };
        }
    }
}
