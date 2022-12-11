using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/11
    /// </summary>
    internal class Day11 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day11(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            return Solve(20, x => x / 3);
        }

        public long Part2()
        {
            return Solve(10_000, x => x % (2*3*5*7*11*13*17*19));
        }

        private long Solve(int loops, Func<long, long> stressLoweringMechanism)
        {
            var monkeys = ParseInput(inputProvider.GetInput()).ToList();

            for (int i = 0; i < loops; i++)
            {
                foreach (var monkey in monkeys)
                {
                    var items = monkey.Items;

                    foreach (var item in items)
                    {
                        var stressLevel = monkey.GetOperationResult(item);
                        var nextMonkey = monkeys[monkey.Next(stressLevel)];
                        monkey.PassItemto(stressLoweringMechanism(stressLevel), nextMonkey);
                        monkey.Activity++;
                    }
                    monkey.Items.Clear();
                }
            }

            return monkeys.OrderByDescending(x => x.Activity).Take(2).Product(x => x.Activity);
        }

        private IEnumerable<Monkey> ParseInput(IEnumerable<string> input)
        {
            return input.Chunk(7).Select(x => Monkey.Parse(x.ToArray()));
        }

        private class Monkey
        {
            public int Id { get; set; }
            public List<long> Items { get; set; }
            public string Operation { get; set; }
            public long TestNumber { get; set; }
            public int TestTrueMonkeyId { get; set; }
            public int TestFalseMonkeyId { get; set; }
            public long Activity { get; set; }

            public static Monkey Parse(string[] monkeyDefs)
            {
                return new Monkey
                {
                    Id = int.Parse(monkeyDefs[0]["Monkey ".Length..^1]),
                    Items = monkeyDefs[1]["  Starting items: ".Length..^0].Split(", ").Select(x => long.Parse(x)).ToList(),
                    Operation = monkeyDefs[2]["  Operation: new = ".Length..^0],
                    TestNumber = int.Parse(monkeyDefs[3]["  Test: divisible by ".Length..^0]),
                    TestTrueMonkeyId = int.Parse(monkeyDefs[4]["    If true: throw to monkey ".Length..^0]),
                    TestFalseMonkeyId = int.Parse(monkeyDefs[5]["    If false: throw to monkey ".Length..^0]),
                };
            }

            public long GetOperationResult(long item)
            {
                var parts = Operation.Split(' ');
                var item2 = char.IsDigit(parts[2][0]) ? long.Parse(parts[2]) : item;
                var op = parts[1][0];
                return Calc(op, item, item2);
            }

            public int Next(long value) => value % TestNumber == 0 ? TestTrueMonkeyId : TestFalseMonkeyId;
            private long Calc(char op, long item1, long item2) => op == '*' ? item1 * item2 : item1 + item2;
            public void PassItemto(long item, Monkey monkey) => monkey.Items.Add(item);
        }
    }
}
