using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day20 : IPartOne<long>, IPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day20(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1() => MixUsingCircularBuffer(1, 1);

        public long Part2() => MixUsingLinkedList(10, 811589153L);

        // Solution 1: Use a circular buffer.
        private long MixUsingCircularBuffer(int mixCount, long factor)
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

        // Solution 2: Use a linked list.
        private long MixUsingLinkedList(int mixCount, long factor)
        {
            var (root, list) = MakeLinkedList(factor);

            for (int round = 0; round < mixCount; round++)
            {
                foreach (var node in list)
                {
                    if (node.Value == 0)
                        continue;

                    var steps = Mod(node.Value, Node.Length - 1);
                    var current = node;
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;

                    for (int i = 0; i < steps; i++)
                        current = current.Next;

                    node.Prev = current;
                    node.Next = current.Next;
                    node.Next.Prev = node;
                    current.Next = node;
                }
            }

            return Sum(root, Offset0(root));
        }

        private int Offset0(Node node)
        {
            int offset = 0;
            for (; node.Value != 0; node = node.Next)
                offset++;

            return offset;
        }

        private long Sum(Node node, int offset)
        {
            long sum = 0;
            
            for (long i = 0; i <= offset + 3000; i++, node = node.Next)
            {
                if ((offset + 1000) == i || (offset + 2000) == i || (offset + 3000) == i)
                    sum += node.Value;
            }

            return sum;
        }

        private long Mod(long x, long m) => (x%m + m)%m;

        public void Swap<T>(T[] arr, int index1, int index2)
        {
            var temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }

        private (Node, Node[]) MakeLinkedList(long factor)
        {
            var arr = inputProvider.GetInput().Select((x) => long.Parse(x) * factor).ToArray();
            var l = new List<Node>();

            Node first = new Node() { Pos = 0, Value = arr[0]  };
            Node current = first;
            l.Add(first);

            for (int i = 1; i < arr.Length; i++)
            {
                Node n = new Node()
                {
                    Pos = i,
                    Value = arr[i],
                    Prev = current
                };

                current.Next = n;
                current = current.Next;
                Node.Length++;
                l.Add(n);
            }

            current.Next = first;
            first.Prev = current;

            return (first, l.ToArray());
        }

        [DebuggerDisplay("[{Value} | {Pos}]")]
        private class Node
        {
            public static int Length { get; set; } = 1;
            public int Pos { get; set; }
            public long Value { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
        }
    }
}
