using AdventOfCode.Extensions;
using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day05 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day05(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();
            var seeds = input[0].NumsLong();
            var maps = input.Skip(2)
                            .Split(string.IsNullOrWhiteSpace)
                            .Select(x => x.Skip(1).Select(y => { var a = y.NumsLong().ToArray(); return (dest: a[0], src: a[1], range: a[2]); }));
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

            var input = inputProvider.GetInput();
            var seeds = input[0].NumsLong().Chunk(2);
            var maps = input.Skip(2).Split(string.IsNullOrWhiteSpace).Select(x => x.Skip(1).Select(y => { var a = y.NumsLong().ToArray(); return (dest: a[0], src: a[1], range: a[2]); }));
            long lowest = long.MaxValue;

            Parallel.ForEach(seeds, (seed) =>
            {
                var (localLowestSeed, localLowestValue) = FindInSubrange(seed[0], seed[0] + seed[1], 10_000, maps);
                var (globalLowestSeed, globalLowestValue) = FindInSubrange(localLowestSeed - 10_000, localLowestSeed, 1, maps);

                lowest = globalLowestValue < lowest ? globalLowestValue : lowest;
            });

            return lowest;
        }

        static (long seed, long lowest) FindInSubrange(long start, long end, long increment, IEnumerable<IEnumerable<(long dest, long src, long range)>> maps)
        {
            var lowest = long.MaxValue;
            var seed = 0L;

            for (var i = start; i < end; i += increment)
            {
                long mapped = i;

                foreach (var map in maps)
                {
                    foreach (var range in map)
                    {
                        if (mapped >= range.src && mapped < range.src + range.range)
                        {
                            mapped += range.dest - range.src;
                            break;
                        }
                    }
                }

                if (mapped < lowest)
                {
                    lowest = mapped;
                    seed = i;
                }
            }

            return (seed, lowest);
        }
    }
}
