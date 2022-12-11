using AdventOfCode.Quizzes.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test.Y2022
{
    public class TestDay07 : TestBase
    {
        [Fact]
        public void Part1()
        {
            var a = new Day07(InputProvider);
            Assert.Equal(95437, a.Part1());
        }

        [Fact]
        public void Part2()
        {
            var a = new Day07(InputProvider);
            Assert.Equal(24933642, a.Part2());
        }

        protected override string[] AocInput()
        {
            return new[] { 
                "$ cd /",
                "$ ls",
                "dir a",
                "14848514 b.txt",
                "8504156 c.dat",
                "dir d",
                "$ cd a",
                "$ ls",
                "dir e",
                "29116 f",
                "2557 g",
                "62596 h.lst",
                "$ cd e",
                "$ ls",
                "584 i",
                "$ cd ..",
                "$ cd ..",
                "$ cd d",
                "$ ls",
                "4060174 j",
                "8033020 d.log",
                "5626152 d.ext",
                "7214296 k",
            };
        }
    }
}
