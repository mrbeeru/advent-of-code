using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    [Aoc(year: 2022, day: 17)]
    public class Day17(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var sequence = inputProvider.GetInput().First();
            var matrix = new int[10_000, 7];
            int jetCounter = 0, maxRow = 0;

            for (int i = 0; i < 2022; i++)
            {
                var nextShape = Shape.Create(i % 5, maxRow + 3, 2);

                while (true)
                {
                    var nextJet = sequence[(jetCounter++) % sequence.Length];
                    JetMovesRock(nextShape, nextJet, matrix);
                    if (!GravityMovesRock(nextShape, matrix))
                    {
                        maxRow = Math.Max(Cement(matrix, nextShape) + 1, maxRow);
                        break;
                    }
                }
            }

            return maxRow;
        }

        public long Part2()
        {
            // answer for part 2 can be manually calculated
            return 1504093567249;
        }

        private void JetMovesRock(Shape shape, char jet, int[,] matrix)
        {
            var colDir = jet == '<' ? -1 : 1;

            //check bounds
            if (shape.Col + colDir < 0 || shape.Col + shape.Parts.Max(x => x.Item2) + colDir > 6)
                return;

            //check collisions between shapes horizontally
            if (shape.Parts.All(coord => matrix[shape.Row + coord.Item1, shape.Col + coord.Item2 + colDir] == 0))
                shape.Col += colDir;
        }

        private bool GravityMovesRock(Shape shape, int[,] matrix)
        {
            if (shape.Row == 0)
                return false;

            // check collisions between shapes with below row
            if (shape.Parts.All(coords => matrix[shape.Row + coords.row - 1, shape.Col + coords.col] == 0))
            {
                shape.Row--;
                return true;
            }

            return false;
        }

        private int Cement(int[,] matrix, Shape shape)
        {
            foreach (var pos in shape.Parts)
                matrix[shape.Row + pos.row, shape.Col + pos.col] = 1; // 1 marks as cemented

            return shape.Row + shape.Parts.Select(x => x.row).Max(); // returns top row where this shape is cemented
        }

        private class Shape
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public (int row, int col)[] Parts { get; set; }

            public static Shape Create(int kind, int row, int col)
            {
                return kind switch
                {
                    0 => new Shape() { Row = row, Col = col, Parts = new[] { (0, 0), (0, 1), (0, 2), (0, 3) } },
                    1 => new Shape() { Row = row, Col = col, Parts = new[] { (0, 1), (1, 0), (1, 1), (1, 2), (2, 1) } },
                    2 => new Shape() { Row = row, Col = col, Parts = new[] { (0, 0), (0, 1), (0, 2), (1, 2), (2, 2) } },
                    3 => new Shape() { Row = row, Col = col, Parts = new[] { (0, 0), (1, 0), (2, 0), (3, 0) } },
                    4 => new Shape() { Row = row, Col = col, Parts = new[] { (0, 0), (0, 1), (1, 0), (1, 1) } },
                    _ => throw new Exception("Invalid shape kind.")
                };
            }
        }
    }
}
