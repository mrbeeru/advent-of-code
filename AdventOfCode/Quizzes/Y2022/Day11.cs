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
    internal class Day11 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day11(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var monkeys = ParseInput(inputProvider.GetInput()).ToList();

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    var items = monkey.Items;

                    foreach (var item in items)
                    {
                        var result = monkey.GetOperationResult(item);
                        var nextMonkey = monkeys[monkey.Test(result) ? monkey.TestTrueMonkeyId : monkey.TestFalseMonkeyId];
                        monkey.PassItemto(result, nextMonkey);
                        monkey.Activity++;
                    }
                    monkey.Items.Clear();
                }
            }

            return monkeys.OrderByDescending(x => x.Activity)
                          .Take(2)
                          .Aggregate(1L, (result, monkey) => result * monkey.Activity);
        }

        private IEnumerable<Monkey> ParseInput(IEnumerable<string> input)
        {
            var groups = input.GroupCount(7);
            var monkeys = new List<Monkey>();

            foreach (var monkeyDefinition in groups)
            {
                var m = monkeyDefinition.ToArray();
                var monkeyId = int.Parse(m[0]["Monkey ".Length..^1]);
                var items = m[1]["  Starting items: ".Length..^0].Split(", ").Select(x => long.Parse(x));
                var operation = m[2]["  Operation: new = ".Length..^0];
                var test = int.Parse(m[3]["  Test: divisible by ".Length..^0]);
                var testTrueMonkeyId = int.Parse(m[4]["    If true: throw to monkey ".Length..^0]);
                var testFalseMonkeyId = int.Parse(m[5]["    If false: throw to monkey ".Length..^0]);

                var monkey = new Monkey
                {
                    Id = monkeyId,
                    Items = items.ToList(),
                    Operation = operation,
                    TestNumber = test,
                    TestTrueMonkeyId = testTrueMonkeyId,
                    TestFalseMonkeyId = testFalseMonkeyId,

                };

                monkeys.Add(monkey);
            }

            return monkeys;
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

            public bool Test(long value)
            {
                return value % TestNumber == 0;
            }

            public long GetOperationResult(long item)
            {
                var a = Operation.Split(' ');
                var item1 = item;
                var item2 = char.IsDigit(a[2][0]) ? long.Parse(a[2]) : item1;
                var op = a[1][0];

                return Calc(op, item1, item2) / 3;
            }

            private long Calc(char op, long item1, long item2)
            {
                return op == '*' ? item1 * item2 : item1 + item2;
            }

            public void PassItemto(long item, Monkey monkey)
            {
                monkey.Items.Add(item);
            }
        }
    }
}
