using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/4
    /// </summary>
    [Aoc(year: 2022, day: 4)]
    public class Day04(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var lines = inputProvider.GetInput();

            return lines.Select(pair => ToRange(pair))
                        .Where(y => Includes(y.range1, y.range2) || Includes(y.range2, y.range1))
                        .Count();
        }

        public long Part2()
        {
            var lines = inputProvider.GetInput();

            return lines.Select(pair => ToRange(pair))
                        .Where(y => Overlap(y.range1, y.range2))
                        .Count();
        }

        private (Range range1, Range range2) ToRange(string sectionPair)
        {
            var pairs = sectionPair.Split(',');
            var r1 = pairs[0].Split('-').Select(x => int.Parse(x)).ToArray();
            var r2 = pairs[1].Split('-').Select(x => int.Parse(x)).ToArray();

            return (new Range(r1[0], r1[1]), new Range(r2[0], r2[1]));
        }

        // Checks whether range 1 includes range 2.
        private bool Includes(Range range1, Range range2)
        {
            return range1.Start.Value <= range2.Start.Value &&
                   range1.End.Value >= range2.End.Value;
        }

        private bool Overlap(Range range1, Range range2)
        {
            return range1.Start.Value <= range2.End.Value &&
                   range2.Start.Value <= range1.End.Value;
        }
    }
}
