using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day24 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;
        private readonly (int row, int col)[] directions = new[] {(-1, 0), (1, 0) , (0, -1) , (0, 1) };

        public Day24(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            var (blizzards, rows, cols) = Parse();
            var activeState = new HashSet<(int, int)> { (0, 1) };
            var endPosition = (26,120);
            return FindSmallestTime(blizzards, activeState, (rows, cols), endPosition);
        }

        public long Part2()
        {
            var (blizzards, rows, cols) = Parse();

            //make 3 trips: start -> finish -> start -> finish
            var activeState = new HashSet<(int, int)> { (0, 1) };
            var endPosition = (26, 120);
            var time =  FindSmallestTime(blizzards, activeState, (rows, cols), endPosition);

            endPosition = (0, 1);
            activeState = new HashSet<(int, int)> { (26, 120) };
            time += FindSmallestTime(blizzards, activeState, (rows, cols), endPosition);

            activeState = new HashSet<(int, int)> { (0, 1) };
            endPosition = (26, 120);
            time += FindSmallestTime(blizzards, activeState, (rows, cols), endPosition);

            return time;
        }

        private int FindSmallestTime(List<Blizzard> blizzards, HashSet<(int row, int col)> activeState, (int rows, int cols) dims, (int row, int col) endPosition)
        {
            int time = 0;

            while(!activeState.Contains(endPosition))
            {
                UpdateBlizzardPositions(blizzards, dims);

                //expand, runs slow because of this
                foreach (var state in activeState.ToList())
                {
                    foreach (var dir in directions)
                    {
                        var nextRow = state.row + dir.row;
                        var nextCol = state.col + dir.col;

                        //found the goal
                        if ((nextRow, nextCol) == endPosition)
                            return time + 1;

                        if (blizzards.Any(x => x.Row == nextRow && x.Col == nextCol) || (nextRow < 1 || nextCol < 1 || nextRow >= dims.rows - 1 || nextCol >= dims.cols - 1))
                            continue;

                        activeState.Add((nextRow, nextCol));
                    }
                }

                //remove positions lost in the blizzard
                activeState.RemoveWhere(x => blizzards.Any(y => y.Row == x.row && y.Col == x.col));
                time++;
                Console.WriteLine($"Time: {time}");
            }

            throw new Exception("Will loop indefinitely anyway so this won't be ever thrown.");
        }

        private (List<Blizzard>, int rows, int cols) Parse()
        {
            var lines = inputProvider.GetInput().ToArray();

            int rows = lines.Count();
            int columns = lines.First().Count();
            var output = new List<Blizzard>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (lines[i][j] == '<')
                        output.Add(new Blizzard { Col = j, Row = i, Direction = (0, -1) });

                    if (lines[i][j] == '>')
                        output.Add(new Blizzard { Col = j, Row = i, Direction = (0, 1) });

                    if (lines[i][j] == '^')
                        output.Add(new Blizzard { Col = j, Row = i, Direction = (-1, 0) });

                    if (lines[i][j] == 'v')
                        output.Add(new Blizzard { Col = j, Row = i, Direction = (1, 0) });
                }
            }

            return (output, rows, columns);
        }

        private void UpdateBlizzardPositions(List<Blizzard> blizzards, (int rows, int cols) dims)
        {
            foreach (var blizzard in blizzards)
            {
                blizzard.Row = blizzard.Row + blizzard.Direction.row;
                blizzard.Col = blizzard.Col + blizzard.Direction.col;

                if (blizzard.Row == 0)
                    blizzard.Row = dims.rows - 2;

                if (blizzard.Col == 0)
                    blizzard.Col = dims.cols - 2;

                if (blizzard.Row == dims.rows - 1)
                    blizzard.Row = 1;

                if (blizzard.Col == dims.cols - 1)
                    blizzard.Col = 1;
            }
        }

        [DebuggerDisplay("[{Row} {Col}]")]
        private class Blizzard
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public (int row, int col) Direction { get; set; }
        }
    }
}
