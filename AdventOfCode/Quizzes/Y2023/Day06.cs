﻿using AdventOfCode.Extensions;
using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 6)]
    public class Day06(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput().Select(x => x.Nums());
            var races = input.First().Zip(input.Last()).ToArray();

            return races.Aggregate(1L, (ways, race) => ways * FindNumWaysToWin(race.First, race.Second));
        }

        public long Part2()
        {
            var input = inputProvider.GetInput()
                .Select(x => x.Nums().Select(num => num.ToString()))
                .Select(x => long.Parse(string.Join("", x)))
                .ToArray();

            return FindNumWaysToWin(input[0], input[1]);
        }

        static int FindNumWaysToWin(long time, long distance)
        {
            var waysToWinRace = 0;

            for (int i = 0; i <= time; i++)
            {
                waysToWinRace += i * (time - i) > distance ? 1 : 0;
            }

            return waysToWinRace;
        }
    }
}
