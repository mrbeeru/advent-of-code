using AdventOfCode.Reader;
using MoreLinq;

namespace AdventOfCode.Quizzes.Y2023
{
    [Aoc(year: 2023, day: 13)]
    public class Day13(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput().Split("").Select(x => x.Select(y => y.ToArray()).ToArray()).ToArray();
            var sum = 0;
            foreach (var mirror in input)
            {
                var (hlines, vlines) = GetPotentialLines(mirror);
                var hline = hlines.Where(x => VerifyHLine(x, mirror));
                var vline = vlines.Where(x => VerifyVLine(x, mirror));

                if (hline.Any())
                {
                    sum += 100 * (hline.First() + 1);
                }


                if (vline.Any())
                {
                    sum += (vline.First() + 1);
                }
            }

            return sum;
        }

        public long Part2()
        {
            var input = inputProvider.GetInput().Split("").Select(x => x.Select(y => y.ToArray()).ToArray()).ToArray();
            var sum = 0;
            foreach (var mirror in input)
            {
                var (hlines1, vlines1) = GetPotentialLines(mirror);
                var (hlines2, vlines2) = GetPotentialLines(mirror, 1);

                hlines2 = hlines2.Where(x => VerifyHLine(x, mirror, 1)).ToList();
                vlines2 = vlines2.Where(x => VerifyVLine(x, mirror, 1)).ToList();

                if (hlines2.Count == 2)
                {
                    hlines2 = hlines2.Except(hlines1).ToList();
                }

                if (vlines2.Count == 2)
                {
                    vlines2 = vlines2.Except(vlines1).ToList();
                }

                if (hlines2.Count > 0)
                {
                    sum += 100 * (hlines2.First() + 1);
                }

                if (vlines2.Count > 0)
                {
                    sum += (vlines2.First() + 1);
                }
            }

            return sum;
        }

        static bool VerifyHLine(int line, char[][] matrix, int flips = 0)
        {
            int offset = 0;
            int numFlips = 0;

            while (true)
            {
                for (int j = 0; j < matrix[line].Length; j++)
                {
                    if (matrix[line + offset + 1][j] != matrix[line - offset][j])
                    {
                        numFlips++;
                    }

                    if (numFlips > flips)
                    {
                        return false;
                    }
                }

                offset++;

                if (line + offset + 1 == matrix.Length)
                {
                    return flips == numFlips;
                }

                if (line - offset == -1)
                {
                    return flips == numFlips;
                }
            }
        }

        static bool VerifyVLine(int line, char[][] matrix, int maxDiffs = 0)
        {
            int offset = 0;
            int diffs = 0;

            while (true)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (matrix[j][line + offset + 1] != matrix[j][line - offset])
                    {
                        diffs++;
                    }

                    if (diffs > maxDiffs)
                    {
                        return false;
                    }
                }

                offset++;

                if (line + offset + 1 == matrix[0].Length)
                {
                    return diffs == maxDiffs;
                }

                if (line - offset == -1)
                {
                    return diffs == maxDiffs;
                }
            }
        }

        static (List<int> hLines, List<int> vLines) GetPotentialLines(char[][] mirror, int maxDiffs = 0)
        {
            var hLines = new List<int>();
            var vLines = new List<int>();

            // check horizontal
            for (var i = 0; i < mirror.Length - 1; i++)
            {
                var diffs = 0;

                for (var j = 0; j < mirror[i].Length; j++)
                {

                    if (mirror[i][j] != mirror[i+1][j])
                    {
                        diffs++;
                    }
                }

                if (diffs <= maxDiffs)
                    hLines.Add(i);
            }

            // check vertical
            for (var i = 0; i < mirror[0].Length - 1; i++)
            {
                var diffs = 0;
                for (var j = 0; j < mirror.Length; j++)
                {

                    if (mirror[j][i] != mirror[j][i + 1])
                    {
                        diffs++;
                    }
                }

                if (diffs <= maxDiffs)
                    vLines.Add(i);
            }

            return (hLines, vLines);
        }

    }
}
