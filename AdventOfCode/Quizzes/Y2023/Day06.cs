using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2023
{
    public class Day06 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day06(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

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
