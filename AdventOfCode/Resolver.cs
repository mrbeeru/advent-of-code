using AdventOfCode.Quizzes;
using AdventOfCode.Reader;
using MoreLinq;
using System.Reflection;

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

            var targetClass = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type =>
                {
                    var attribute = type.GetCustomAttribute<AocAttribute>();
                    return attribute?.Year == year && attribute?.Day == day;
                });

            if (targetClass == null)
                throw new Exception($"AOC {year}-{day:00} is not implemented.");

            var method = targetClass.GetMethod($"Part{part}");

            if (method == null)
                throw new Exception($"AOC {year}-{day:00} Part {part} is not implemented.");

            var instance = Activator.CreateInstance(targetClass, new object[] { provider });

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

                var part1 = interfaces.Where(x => x.FullName.Contains("IPartOne")).Any();
                var part2 = interfaces.Where(x => x.FullName.Contains("IPartTwo")).Any();

                hints.Add((year, day, part1, part2));
            }

            var groupedByYears = hints.GroupBy(x => x.year);
            string result = "";

            foreach (var year in groupedByYears)
            {
                result += Environment.NewLine + year.Key + Environment.NewLine;

                foreach (var tuple in year)
                {
                    result += "    Day " + $"{tuple.day}".PadRight(2) + " -  " + (tuple.part1 ? "Part 1 " : "") + (tuple.part1 && tuple.part2 ? "| " : "") + (tuple.part2 ? "Part 2" : "");
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
