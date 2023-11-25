using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    [Verb("run", HelpText = "Runs the specified quizz.")]
    internal class Options
    {
        [Option('y', "year", Required = true, HelpText = "Advent of Code year.", SetName = "grp")]
        public int Year { get; set; }

        [Option('d', "day", Required = true, HelpText = "Advent of Code day.", SetName = "grp")]
        public int Day { get; set; }

        [Option('p', "part", Required = true, HelpText = "Advent of Code part.", SetName = "grp")]
        public int Part { get; set; }
    }

    [Verb("ls", HelpText = "Displays a list of all available quizzes.")]
    internal class ShowVerb
    {
    }
}
