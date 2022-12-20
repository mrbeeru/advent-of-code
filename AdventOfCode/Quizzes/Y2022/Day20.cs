using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day20 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day20(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1() => Mix(1, 1);

        public long Part2() => Mix(10, 811589153L);

        private long Mix(int mixCount, long factor)
        {
            var arr = inputProvider.GetInput().Select(long.Parse).Select(x => x * factor).ToArray();
            var idx = Enumerable.Range(0, arr.Length).ToArray();

            for (int round = 0; round < mixCount; round++)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var ci = idx.Select((x, idx) => (x, idx)).Single(x => x.x == i).idx;
                    var val = Mod(arr[ci], arr.Length - 1);

                    for (int j = 0; j < val; j++)
                    {
                        var ni = (ci + 1) % arr.Length;

                        if (ni == -1)
                        {
                            for (int k = 1; k < arr.Length; k++)
                            {
                                Swap(arr, k-1, k);
                                Swap(idx, k-1, k);
                            }

                            ci = arr.Length - 1;
                            ni = arr.Length;
                        }

                        Swap(arr, ci, ni);
                        Swap(idx, ci, ni);
                        ci = ni;
                    }
                }
            }

            var indexOf0 = arr.Select((x, idx) => (x, idx)).Single(x => x.x == 0).idx;

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
        
        long Mod(long x, long m) => (x%m + m)%m;
    }
}
