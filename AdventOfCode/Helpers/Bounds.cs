using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    internal static class Bounds
    {
        public static bool Contains(int width, int height, int row, int col) 
        {
            return col >= 0 && col < width && row >= 0 && row < height;
        }

        public static bool Contains<T>(T[][] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[^1].Length;
        }
    }
}
