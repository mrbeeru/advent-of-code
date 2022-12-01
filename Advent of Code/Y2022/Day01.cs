using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2022
{
    internal class Day01
    {
        private IEnumerable<long> GetCaloriesPerElf()
        {
            var calories = new List<long>();
            var lines = File.ReadAllLines("Y2022/input01.txt");
            var current = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    calories.Add(current);
                    current = 0;
                    continue;
                }

                current += int.Parse(line);
            }

            return calories;
        }

        public long Part1()
        {
            return GetCaloriesPerElf().Max();
        }

        public long Part2()
        {
            return GetCaloriesPerElf()
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();
        }
    }
}
