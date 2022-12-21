using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay21 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day21(InputProvider);
            Assert.Equal(152, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day21(InputProvider);
            Assert.Equal(301, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[]
            {
                "root: pppw + sjmn",
                "dbpl: 5",
                "cczh: sllz + lgvd",
                "zczc: 2",
                "ptdq: humn - dvpt",
                "dvpt: 3",
                "lfqf: 4",
                "humn: 5",
                "ljgn: 2",
                "sjmn: drzm * dbpl",
                "sllz: 4",
                "pppw: cczh / lfqf",
                "lgvd: ljgn * ptdq",
                "drzm: hmdt - zczc",
                "hmdt: 32",
            };
        }
    }
}
