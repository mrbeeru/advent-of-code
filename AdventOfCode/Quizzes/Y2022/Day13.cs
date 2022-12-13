using AdventOfCode.Reader;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day13 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        public Day13(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = inputProvider.GetInput();
            var groups = input.Split(string.Empty);
            var parsed = groups.Select(x => x.Select(y => Parse(y)).ToArray()).ToList();
            var res = parsed.Select((x, i) => (x[0].CompareTo(x[1]), i + 1)).Where(x => x.Item1 == 1);
            return res.Sum(x => x.Item2);
        }

        public bool Compare(string a, string b)
        {
            return false;
        }

        private Node Parse(string a)
        {
            Node root = new Node();
            Node current = root;

            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] == '[')
                {
                    var tmp = new Node();
                    current.Next.Add(tmp);
                    tmp.Parent = current;
                    current = tmp;
                } else if (a[i] == ']')
                {
                    current = current.Parent;
                } else if (a[i] == ',')
                {
                    continue;
                } else if (char.IsDigit(a[i]))
                {
                    var num = NextNumber(a, i);
                    i += num/10;
                    current.Next.Add(num);
                } else
                {
                    throw new Exception("Unhandled.");
                }
            }

            return root;
        }

        private class Node : IComparable<Node>
        {
            public Node Parent;
            public List<object> Next { get; set; } = new List<object>();

            public int CompareTo(Node? other)
            {
                for (int i = 0; i < Next.Count; i++)
                {
                    if (i >= other.Next.Count)
                        return -1;

                    object obj = Next[i];
                    object obj2 = other.Next[i];

                    if (obj is int numLeft && obj2 is int numRight)
                    {
                        if (numLeft > numRight)
                            return -1;
                        else if (numLeft < numRight)
                            return 1;
                    }

                    if (obj is Node nodeLeft && obj2 is Node nodeRight)
                    {
                        return nodeLeft.CompareTo(nodeRight);
                    }

                    if (obj is Node)
                    {
                        return ((Node)obj).CompareTo(new Node { Next = new List<object> { obj2 } });
                    }

                    if (obj2 is Node)
                    {
                        return new Node { Next = new List<object> { obj } }.CompareTo((Node)obj2);
                    }
                }

                return 1;
            }
        }

        private int NextNumber(string a, int index)
        {
            if (!char.IsDigit(a[index]))
                throw new Exception($"No digit at pos {index}");

            int result = 0;

            while (char.IsDigit(a[index]))
            {
                result *= 10;
                result += a[index] - '0';
                index++;
            }

            return result;
        }

    }
}
