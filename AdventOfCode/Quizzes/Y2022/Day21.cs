using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using System.Diagnostics;

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

            var me = root.Right;
            var other = root.Left;
            human.Value = 0; //change human value to see if we are left of root or right of root

            if (root.Right.TotalValue == me.TotalValue)
            {
                me = root.Left;
                other = root.Right;
            }

            long UB = long.MaxValue;
            long LB = 0;
            var guess = UB/2;
            var val = me.TotalValue;
            human.Value = 100; //change human value to see if root value goes up or down
            var direction = me.TotalValue > val ? 1 : -1;

            while (me.TotalValue != other.TotalValue)
            {
                human.Value = guess;

                if (me.TotalValue < other.TotalValue)
                {
                    UB = direction == -1 ? Math.Min(UB, guess) : UB;
                    LB = direction == +1 ? Math.Max(LB, guess) : LB;
                    guess = (LB + UB) / 2;
                }
                else if (me.TotalValue > other.TotalValue)
                {
                    UB = direction == +1 ? Math.Min(UB, guess) : UB;
                    LB = direction == -1 ? Math.Max(LB, guess) : LB;
                    guess = (LB + UB) / 2;
                }
                else
                    return guess;
            }

            return human.Value;
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
