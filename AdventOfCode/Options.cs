using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Options
    {
        [Option('y', "year", Required = true, HelpText = "Advent of Code year.")]
        public int Year { get; set; }

        [Option('d', "day", Required = true, HelpText = "Advent of Code day.")]
        public int Day { get; set; }

        [Option('p', "part", Required = true, HelpText = "Advent of Code part.")]
        public int Part { get; set; }

        [Option("filepath", Required = false, HelpText = "Advent of Code input from file.")]
        public string FilePath { get; set; }
    }
}
