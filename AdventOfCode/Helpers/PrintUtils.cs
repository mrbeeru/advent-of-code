using System.Diagnostics;

namespace AdventOfCode.Helpers
{
    public static class PrintUtils
    {
        public static void Print<T>(this T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                    Debug.Write(matrix[i, j]);
                }
                Console.WriteLine();
                Debug.WriteLine("");
            }

            Console.WriteLine();
            Debug.WriteLine("");
        }

        public static void Print<T>(this T[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j]);
                    Debug.Write(matrix[i][j]);
                }
                Console.WriteLine();
                Debug.WriteLine("");
            }

            Console.WriteLine();
            Debug.WriteLine("");
        }
    }
}
