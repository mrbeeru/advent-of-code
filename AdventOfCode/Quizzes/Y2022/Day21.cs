using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day21 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day21(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            (Monkey root, _) = Parse();
            return (long)root.TotalValue;
        }

        public long Part2()
        {
            (Monkey root, Monkey human) = Parse();

            var guess = 1L;
            long UB = long.MaxValue - 1;
            long LB = 0;

            // works only on my input, need to determine the inequalities
            while (root.Right.TotalValue != root.Left.TotalValue)
            {
                human.Value = guess;

                if (root.Right.TotalValue < root.Left.TotalValue)
                {
                    LB = Math.Max(LB, guess);
                    guess = (LB + UB) / 2;
                }
                else if (root.Right.TotalValue > root.Left.TotalValue)
                {
                    UB = Math.Min(UB, guess);
                    guess = (LB + UB) / 2;

                }
                else
                    return guess;
            }

            throw new Exception("Something went wrong.");
        }

        private (Monkey root, Monkey human) Parse()
        {
            var lines = inputProvider.GetInput();
            Dictionary<string, Monkey> monkeys = new Dictionary<string, Monkey>();

            foreach (var line in lines)
            {
                var monkeyName = line[0..4];
                monkeys.TryGetValue(monkeyName, out Monkey? monkey);
                monkey ??= new Monkey();
                monkey.Name = monkeyName;
                monkeys[monkeyName] = monkey;

                var num = line.Nums();

                if (num.Any())
                    monkey.Value = num.First();
                else
                {
                    monkey.Operator = line[11];

                    var m1 = line[6..10];
                    var m2 = line[13..17];

                    monkeys.TryGetValue(m1, out Monkey? monkeyLeft);
                    monkeys.TryGetValue(m2, out Monkey? monkeyRight);

                    monkeyLeft ??= new Monkey() { Name = m1 };
                    monkeyRight ??= new Monkey() { Name = m2 };
                    monkeys[m1] = monkeyLeft;
                    monkeys[m2] = monkeyRight;

                    monkey.Right = monkeyRight;
                    monkey.Left = monkeyLeft;
                }
            }

            return (monkeys["root"], monkeys["humn"]);
        }

        [DebuggerDisplay("{Name} : {TotalValue}")]
        private class Monkey
        {
            public string Name { get; set; }
            public char Operator { get; set; }
            public Monkey? Left { get; set; }
            public Monkey? Right { get; set; }
            public long Value { get; set; }
            public decimal TotalValue
            {
                get
                {
                    if (Operator == 0)
                        return Value;

                    var a = Value + Operator switch
                    {
                        '+' => Left?.TotalValue + Right?.TotalValue,
                        '-' => Left?.TotalValue - Right?.TotalValue,
                        '*' => Left?.TotalValue * Right?.TotalValue,
                        '/' => Left?.TotalValue / Right?.TotalValue,
                        _ => throw new Exception($"Invalid operator '{Operator}'.")
                    };

                    return a.Value;
                }
            }
        }
    }
}
