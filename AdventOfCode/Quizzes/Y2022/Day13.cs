using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using static MoreLinq.Extensions.SplitExtension;
using static MoreLinq.Extensions.ZipLongestExtension;

namespace AdventOfCode.Quizzes.Y2022
{
    [Aoc(year: 2022, day: 13)]
    public class Day13(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput();
            var groups = input.Split(string.Empty);
            var parsed = groups.Select(x => x.Select(y => Parse(y)).ToArray()).ToList();

            return parsed.Select((x, i) => (x[0].CompareTo(x[1]), i + 1))
                         .Where(x => x.Item1 == -1)
                         .Sum(x => x.Item2);
        }

        public long Part2()
        {
            var dividerPackets = new[] { "[[2]]", "[[6]]" };
            var input = inputProvider.GetInput().ToList();
            input.AddRange(dividerPackets);

            var packets = input.Split(string.Empty).SelectMany(x => x).Select(x => Parse(x)).ToList();
            packets.Sort();
            return packets.Select((x, i) => (x, i)).Where(x => x.x.IsDivider()).Product(x => x.i + 1);
        }

        private Packet Parse(string rawPacket)
        {
            Packet root = new();
            Packet current = root;

            for (int i = 1; i < rawPacket.Length; i++)
            {
                if (rawPacket[i] == '[')
                {
                    var tmp = new Packet() { Parent = current };
                    current.Next.Add(tmp);
                    current = tmp;
                }
                else if (rawPacket[i] == ']')
                {
                    current = current.Parent;
                }
                else if (char.IsDigit(rawPacket[i]))
                {
                    var num = NextNumber(rawPacket, i);
                    current.Next.Add(num);
                    i += num/10;
                }
                else if (rawPacket[i] == ',')
                    continue;
                else
                    throw new Exception("Unhandled.");

            }

            return root;
        }

        private class Packet : IComparable<Packet>
        {
            public Packet Parent;
            public List<object> Next { get; set; } = new List<object>();

            public static Packet ToPacket(object o) => new Packet { Next = new List<object> { o } };

            public int CompareTo(Packet? other)
            {
                return Next.ZipLongest(other.Next, (x, y) => (x, y))
                    .Select(pair => pair switch
                    {
                        (null, _) => -1,
                        (_, null) => 1,
                        (int left, int right) => Math.Sign(left - right),
                        (Packet left, Packet right) => left.CompareTo(right),
                        (Packet left, int right) => left.CompareTo(ToPacket(right)),
                        (int left, Packet right) => ToPacket(left).CompareTo(right),
                        _ => throw new Exception("Invalid case.")
                    })
                    .Append(-1)
                    .First(x => x != 0);
            }

            public bool IsDivider()
            {
                return Parent == null &&
                    Next.Count == 1 &&
                    Next[0] is Packet node &&
                    node.Next.Count == 1 &&
                    node.Next[0] is int a &&
                    (a == 2 || a == 6);
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
