using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day17 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day17(IInputProvider inputProvider)
        {
            this.inputProvider=inputProvider;
        }

        public long Part1()
        {
            var matrix = new int[10_000, 7];
            var sequence = inputProvider.GetInput().First();
            int jetCounter = 0;
            int maxRow = 0;

            for (int i = 0; i < 2022; i++)
            {
                var nextShape = Shape.Create(i % 5);
                nextShape.Col = 2;
                nextShape.Row = maxRow + 3;

                while (true)
                {
                    var nextJet = sequence[(jetCounter++) % sequence.Length];
                    JetMovesRock(nextShape, nextJet, matrix);
                    var test = GravityMovesRock(nextShape, matrix);

                    if (test)
                        continue;
                    
                    maxRow = Math.Max(nextShape.Cement(matrix) + 1, maxRow);
                    break;
                }
            }

            return maxRow;
        }

        private void PrintMatrix(int[,] matrix, int from, int to)
        {
            Console.Clear();
            for (int i = to; i >= from; i--)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(matrix[i, j] == 0 ? '.' : "#");
                }

                Console.WriteLine();
            }
        }

        private void JetMovesRock(Shape shape, char jet, int[,] matrix)
        {
            bool ok = true;


            if (jet == '<')
            {
                // check left wall
                if (shape.Col <= 0)
                    return;

                foreach (var coords in shape.Pos)
                {
                    if (matrix[shape.Row + coords.Item1, shape.Col + coords.Item2 - 1] != 0)
                        ok = false;
                }

                if (ok)
                    shape.Col--;
            }
            else if (jet == '>')
            {
                //check right wall
                if (shape.Col + shape.Pos.Max(x => x.Item2) >= 6)
                    return;

                foreach (var coords in shape.Pos)
                {
                    if (matrix[shape.Row + coords.Item1, shape.Col + coords.Item2 + 1] != 0)
                        ok = false;
                }

                if (ok)
                    shape.Col++;
            }
            else throw new Exception("Invalid jet type.");
        }

        private bool GravityMovesRock(Shape shape, int[,] matrix)
        {
            if (shape.Row == 0)
                return false;

            bool ok = true;
            foreach (var coords in shape.Pos)
            {
                if (matrix[shape.Row + coords.Item1 - 1, shape.Col + coords.Item2] != 0)
                {
                    ok = false;
                }
            }

            if (ok)
            {
                shape.Row--;
            }

            return ok;
        }


        private class Shape
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public List<(int, int)> Pos { get; set; }

            public static Shape Create(int kind, params (int, int)[] pos)
            {
                return kind switch
                {
                    0 => new Shape() { Pos = new[] { (0, 0), (0, 1), (0, 2), (0, 3) }.ToList() },
                    1 => new Shape() { Pos = new[] { (0, 1), (1, 0), (1, 1), (1, 2), (2, 1) }.ToList() },
                    2 => new Shape() { Pos = new[] { (0, 0), (0, 1), (0, 2), (1, 2), (2, 2) }.ToList() },
                    3 => new Shape() { Pos = new[] { (0, 0), (1, 0), (2, 0), (3, 0) }.ToList() },
                    4 => new Shape() { Pos = new[] { (0, 0), (0, 1), (1, 0), (1, 1) }.ToList() },
                    _ => throw new Exception("Invalid shape kind.")
                };
            }

            public int Cement(int[,] matrix)
            {
                foreach (var pos in Pos)
                {
                    matrix[Row + pos.Item1, Col + pos.Item2] = 1;
                }

                return Row + Pos.Select(x => x.Item1).Max();
            }
        }
    }
}
