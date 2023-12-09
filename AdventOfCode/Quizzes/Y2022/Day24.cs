using AdventOfCode.Reader;
using System.Diagnostics;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day24 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;
        private readonly (int row, int col)[] directions = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        public Day24(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            var (blizzards, dims, endPos) = Parse();
            var activeState = new HashSet<(int, int)> { (0, 1) };
            return FindSmallestTime(blizzards, activeState, dims, endPos);
        }

        public long Part2()
        {
            var (blizzards, dims, endPos) = Parse();
            var startPos = (0, 1);
            var activeState = new HashSet<(int, int)> { startPos };
            var time = FindSmallestTime(blizzards, activeState, dims, endPos);

            startPos = endPos;
            activeState = new HashSet<(int, int)> { startPos };
            endPos = (0, 1);
            time += FindSmallestTime(blizzards, activeState, dims, endPos);

            endPos = startPos;
            startPos = (0, 1);
            activeState = new HashSet<(int, int)> { startPos };
            time += FindSmallestTime(blizzards, activeState, dims, endPos);

            return time;
        }

        private int FindSmallestTime(List<Blizzard> blizzards, HashSet<(int row, int col)> activeState, (int rows, int cols) dims, (int row, int col) endPosition)
        {
            int time = 0;

            while (true)
            {
                UpdateBlizzardPositions(blizzards, dims);
                var bhs = blizzards.Select(x => (x.Row, x.Col)).ToHashSet();

                foreach (var state in activeState.ToList())
                {
                    foreach (var dir in directions)
                    {
                        var nextRow = state.row + dir.row;
                        var nextCol = state.col + dir.col;

                        //found the goal
                        if ((nextRow, nextCol) == endPosition)
                            return time + 1;

                        if (bhs.Contains((nextRow, nextCol)) || IsOutOfBounds(nextRow, nextCol, dims))
                            continue;

                        activeState.Add((nextRow, nextCol));
                    }
                }

                //remove positions lost in the blizzard
                activeState.RemoveWhere(bhs.Contains);
                time++;
            }

            throw new Exception("Will loop indefinitely anyway so this won't be ever thrown.");
        }

        private bool IsOutOfBounds(int row, int col, (int rows, int cols) dim)
        {
            return (row < 1 || col < 1 || row >= dim.rows - 1 || col >= dim.cols - 1);
        }

        private (List<Blizzard>, (int rows, int cols), (int row, int col)) Parse()
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

            return (output, (rows, columns), (lines.Length - 1, lines.Last().Length - 2));
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
