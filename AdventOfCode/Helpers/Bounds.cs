namespace AdventOfCode.Helpers
{
    internal static class Bounds
    {
        public static bool Within(int width, int height, int row, int col)
        {
            return col >= 0 && col < width && row >= 0 && row < height;
        }

        public static bool Within<T>(this T[][] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }

        public static bool Within<T>(this (int row, int col) index, T[][] matrix)
        {
            return Within(matrix, index.row, index.col);
        }

        public static bool Within<T>(this Coords2D index, T[][] matrix)
        {
            return Within(matrix, index.X, index.Y);
        }

        public static bool Within<T>(this Coords2D index, T[,] matrix)
        {
            return Within(matrix.GetLength(1), matrix.GetLength(0), index.X, index.Y);
        }
    }
}
