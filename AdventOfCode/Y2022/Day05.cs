using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Y2022
{
    internal class Day05 : IAocDay<string>
    {
        private readonly IInputProvider inputProvider;

        public Day05(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public string Part1()
        {
            return Solve(false);
        }

        public string Part2()
        {
            return Solve(true);
        }

        private string Solve(bool preserveOrder)
        {
            var input = inputProvider.GetInput();
            var emptyLineIndex = input.Select((itm, index) => (itm, index)).First(x => string.IsNullOrWhiteSpace(x.itm)).index;

            var stacks = ParseStacks(input.Take(emptyLineIndex));
            var moves = ParseMoves(input.Skip(emptyLineIndex + 1));
            ExecuteMoves(moves, stacks, preserveOrder);

            return string.Join("", stacks.Select(x => x.Pop()));
        }

        private Stack<char>[] ParseStacks(IEnumerable<string> lines)
        {
            var numStacks = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).Max();
            var stacks = new Stack<char>[numStacks];
            
            Enumerable.Range(0, numStacks).ForEach(x => stacks[x] = new());

            foreach (var line in lines.Take(lines.Count() - 1).Reverse())
            {
                for (int i = 0; i < numStacks; i++)
                {
                    if (!char.IsUpper(line[1 + 4 * i]))
                        continue;

                    stacks[i].Push(line[1 + 4 * i]);
                }
            }

            return stacks;
        }

        private IEnumerable<(int count, int from, int to)> ParseMoves(IEnumerable<string> lines)
        {
            var moves = new List<(int, int, int)>();

            foreach (var move in lines)
            {
                var match = Regex.Match(move, @"move (\d+) from (\d+) to (\d+)");
                var crateCount = int.Parse(match.Groups[1].Value);
                var fromStack = int.Parse(match.Groups[2].Value) - 1;
                var toStack = int.Parse(match.Groups[3].Value) - 1;

                moves.Add((crateCount, fromStack, toStack));
            }

            return moves;
        }

        private void ExecuteMoves(IEnumerable<(int count, int from, int to)> moves, Stack<char>[] stacks, bool preserveOrder)
        {
            foreach (var move in moves)
            {
                if (preserveOrder == false)
                {
                    // for part 1
                    for (int i = 0; i < move.count; i++)
                        stacks[move.to].Push(stacks[move.from].Pop());
                } else
                {
                    //for part 2
                    var list = new List<char>();
                    for (int i = 0; i < move.count; i++)
                        list.Add(stacks[move.from].Pop());

                    list.Reverse();
                    list.ForEach(x => stacks[move.to].Push(x));
                }
            }
        }
    }
}
