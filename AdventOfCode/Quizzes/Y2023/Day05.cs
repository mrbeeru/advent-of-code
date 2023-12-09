using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day05 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day05(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var (seeds, maps) = ReadInput(inputProvider.GetInput());
            long lowest = long.MaxValue;

            foreach (var seed in seeds)
            {
                var (_, value) = FindInSubrange(seed, seed + 1, 1, maps);
                lowest = value < lowest ? value : lowest;
            }

            return lowest;
        }

        public long Part2()
        {
            Console.WriteLine("This might take a few seconds...");

            var (seeds, maps) = ReadInput(inputProvider.GetInput());
            var seedPair = seeds.Chunk(2);
            long lowest = long.MaxValue;

            Parallel.ForEach(seedPair, (seed) =>
            {
                var (localLowestSeed, localLowestValue) = FindInSubrange(seed[0], seed[0] + seed[1], 10_000, maps);
                var (globalLowestSeed, globalLowestValue) = FindInSubrange(localLowestSeed - 10_000, localLowestSeed, 1, maps);

                lowest = globalLowestValue < lowest ? globalLowestValue : lowest;
            });

            return lowest;
        }

        static (long seed, long lowest) FindInSubrange(long start, long end, long increment, IEnumerable<IEnumerable<(long dest, long src, long range)>> maps)
        {
            var lowestValue = long.MaxValue;
            var seed = 0L;

            for (var i = start; i < end; i += increment)
            {
                long mappedValue = i;

                foreach (var map in maps)
                {
                    foreach (var range in map)
                    {
                        if (mappedValue >= range.src && mappedValue < range.src + range.range)
                        {
                            mappedValue += range.dest - range.src;
                            break;
                        }
                    }
                }

                if (mappedValue < lowestValue)
                {
                    lowestValue = mappedValue;
                    seed = i;
                }
            }

            return (seed, lowestValue);
        }

        static (IEnumerable<long> seeds, IEnumerable<IEnumerable<(long dest, long src, long range)>>) ReadInput(IList<string> input)
        {
            var seeds = input[0].NumsLong();
            var maps = input.Skip(2)
                            .Split(string.IsNullOrWhiteSpace)
                            .Select(x => x.Skip(1).Select(y => { var a = y.NumsLong().ToArray(); return (dest: a[0], src: a[1], range: a[2]); }));

            return (seeds, maps);
        }
    }
}
