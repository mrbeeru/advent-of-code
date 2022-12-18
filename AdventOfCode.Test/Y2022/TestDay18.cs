using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay18 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day18(InputProvider);
            Assert.Equal(64, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day18(InputProvider);
            Assert.Equal(58, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] {
                "2,2,2",
                "1,2,2",
                "3,2,2",
                "2,1,2",
                "2,3,2",
                "2,2,1",
                "2,2,3",
                "2,2,4",
                "2,2,6",
                "1,2,5",
                "3,2,5",
                "2,1,5",
                "2,3,5",
            }; 
        }
    }
}
