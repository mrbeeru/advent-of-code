using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay25 : TestBase
    {
        [Fact]
        public void Part1() => Assert.Equal("2=-1=0", new Day25(InputProvider).Part1());

        protected override string[] AocInput()
        {
            return new[]
            {
                "1=-0-2",
                "12111",
                "2=0=",
                "21",
                "2=01",
                "111",
                "20012",
                "112",
                "1=-1=",
                "1-12",
                "12",
                "1=",
                "122",
            };
        }
    }
}
