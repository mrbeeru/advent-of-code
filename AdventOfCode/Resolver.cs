using AdventOfCode.Quizzes;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Instantiates the correspoding AOC day class and method.
    /// </summary>
    internal class Resolver
    {
        public static (object?, MethodInfo) Resolve(int year, int day, int part, IInputProvider provider)
        {
            if (part != 1 && part != 2)
                throw new Exception("Part can be 1 or 2");

            var cls = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Single(x => x.FullName == $"AdventOfCode.Quizzes.Y{year}.Day{day:00}");

            if (cls == null)
                throw new Exception($"AOC {year}-{day:00} is not implemented.");

            var method = cls.GetMethod($"Part{part}");

            if (method == null)
                throw new Exception($"AOC {year}-{day:00} Part {part} is not implemented.");

            var instance = Activator.CreateInstance(cls, new object[] { provider });

            return (instance, method);
        }

        public static void DisplayAvailableQuizzes()
        {
            var hints = new List<(string year, string day, bool part1, bool part2)>();
            var quizzes = Assembly.GetExecutingAssembly().GetTypes().Where(c => (typeof(IQuiz).IsAssignableFrom(c) && c.IsClass));

            foreach (var quiz in quizzes)
            {
                var year = quiz.FullName[22..26];
                var day = quiz.FullName[30..32].TrimStart('0');

                var interfaces = quiz.GetInterfaces();

                var part1 = interfaces.Where(x => x.FullName.Contains("IQuizPartOne")).Any();
                var part2 = interfaces.Where(x => x.FullName.Contains("IQuizPartTwo")).Any();

                hints.Add((year, day, part1, part2));
            }

            var groupedByYears = hints.GroupBy(x => x.year);
            string result = "";

            foreach (var year in groupedByYears)
            {
                result += Environment.NewLine + year.Key + Environment.NewLine;

                foreach (var tuple in year)
                {
                    result += "    Day " + tuple.day + " - " + (tuple.part1 ? "Part 1 " : "") + (tuple.part2 ? "Part 2" : "");
                    result += Environment.NewLine;
                }
            }

            Console.WriteLine(
                Environment.NewLine +
                "Available quizzes" + Environment.NewLine  +
                "-----------------" + Environment.NewLine  +
                result);
        }
    }
}
