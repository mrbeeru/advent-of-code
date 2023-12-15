using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 15)]
    public class Day15(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        private record Lens(string Label, string FocalLen);

        public long Part1() => inputProvider.GetInput().Single().Split(',').Select(HASH).Sum();

        public long Part2()
        {
            var lenses = inputProvider.GetInput().Single().Split(',').Select(x => x.Split(["-", "="], StringSplitOptions.None)).Select(x => new Lens(x[0], x[1]));
            var boxes = new Dictionary<int, List<Lens>>();
            Enumerable.Range(0, 256).ForEach(x => boxes.Add(x, []));

            foreach (var lens in lenses)
            {
                var boxNumber = HASH(lens.Label);
                var list = boxes[boxNumber];
                var items = list.Where(x => x.Label == lens.Label);

                if (lens.FocalLen == "")
                {
                    if (items.Any())
                    {
                        list.Remove(items.First());
                    }
                }
                else if (items.Any())
                {
                    var index = list.IndexOf(items.First());
                    list[index] = lens;
                }
                else
                {
                    list.Add(lens);
                }
            }

            // total focal power
            return boxes.Select(box => box.Value.Select((lenses, index) => (box.Key + 1) * (index + 1) * int.Parse(lenses.FocalLen)).Sum()).Sum();
        }

        static int HASH(string input)
        {
            var hash = 0;

            foreach (var c in input)
            {
                hash += c;
                hash *= 17;
                hash %= 256;
            }

            return hash;
        }
    }
}
