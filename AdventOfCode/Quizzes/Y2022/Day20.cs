using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day20 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day20(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            var arr = inputProvider.GetInput().Select(int.Parse).ToArray();
            var idx = Enumerable.Range(0, arr.Length).ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                var pos = idx.Select((x, i) => (x, i)).Single(x => x.x == i);
                var ci = pos.i;
                var val = arr[ci];

                for (int j = 0; j < Math.Abs(val); j++)
                {
                    var sign = Math.Sign(val);
                    var ni = ci + sign;
                    ni %= arr.Length;

                    if (ni == -1)
                    {
                        for (int k = 1; k < arr.Length; k++)
                        {
                            Swap(arr, k-1, k);
                            Swap(idx, k-1, k);
                        }

                        ci = arr.Length - 1;
                        ni = arr.Length - 1 + sign;
                    }

                    Swap(arr, ci, ni);
                    Swap(idx, ci, ni);
                    ci = ni;
                }
            }

            var indexOf0 = arr.Select((x, i) => (x, i)).Single(x => x.x == 0).i;

            return arr[(indexOf0 + 1000) % arr.Length] + 
                   arr[(indexOf0 + 2000) % arr.Length] +
                   arr[(indexOf0 + 3000) % arr.Length];
        }

        public void Swap<T>(T[] arr, int index1, int index2)
        {
            var temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }
    }
}
