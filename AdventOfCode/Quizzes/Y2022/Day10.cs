using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/10
    /// </summary>
    public class Day10 : IPartOne<long>, IPartTwo<string>
    {
        private readonly IInputProvider inputProvider;
        private readonly char[] crt = Enumerable.Range(0, 240).Select(x => ' ').ToArray();
        private delegate int Handle(int cc, int regx);

        public Day10(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            return Solve(HandlePart1);
        }

        public string Part2()
        {
            Solve(HandlePart2);
            crt.Chunk(40).ForEach(x => Console.WriteLine(string.Join("", x))); // display the crt

            return $"{Environment.NewLine}The answer is the 8 capital letters displayed above.";
        }

        private int Solve(Handle handle)
        {
            var input = inputProvider.GetInput().ToArray();
            int signalStrength = 0, cc = 0, regx = 1, ip = 0;

            for (; ip < input.Length; ip++)
            {
                (var opcode, var value) = ParseInstruction(input[ip]);
                signalStrength += handle(++cc, regx);

                if (opcode == "addx")
                {
                    signalStrength += handle(++cc, regx);
                    regx += value;
                }
            }

            return signalStrength;
        }

        private int HandlePart1(int cc, int regx)
        {
            return (cc + 20) % 40 == 0 ? cc * regx : 0;
        }

        private int HandlePart2(int cc, int regx)
        {
            crt[cc - 1] = (((cc-1) % 40) >= regx-1 && ((cc-1) % 40) <= regx + 1) ? '▓' : ' ';
            return 0;
        }

        private (string opcode, int value) ParseInstruction(string instruction)
        {
            var parts = instruction.Split();
            var value = parts.Length > 1 ? int.Parse(parts[1]) : 0;
            return (parts[0], value);
        }
    }
}
