using AdventOfCode.Helpers;
using AdventOfCode.Reader;
using MoreLinq;
using QuikGraph;
using QuikGraph.Algorithms;

namespace AdventOfCode.Quizzes.Y2022
{
    [Aoc(year: 2022, day: 12)]
    public class Day12(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var input = inputProvider.GetInput();
            var end = input.SelectMany(x => x).Select((x, i) => (x, i)).Where(x => x.x == 'E').Single().i;
            var start = input.SelectMany(x => x).Select((x, i) => (x, i)).Where(x => x.x == 'S').Single().i;
            var graph = BuildGraph(input);
            return FindNumberOfSteps(graph, start, end);
        }

        public long Part2()
        {
            var input = inputProvider.GetInput();
            var end = input.SelectMany(x => x).Select((x, i) => (x, i)).Where(x => x.x == 'E').Single().i;
            var possibleStarts = input.SelectMany(x => x).Select((c, i) => (c, i)).Where(x => x.c == 'a' || x.i == 'S').Select(x => x.i);
            var graph = BuildGraph(input);
            return possibleStarts.Select(x => FindNumberOfSteps(graph, x, end)).Where(steps => steps > 0).Min();
        }

        private int FindNumberOfSteps(AdjacencyGraph<int, Edge<int>> graph, int start, int end)
        {
            var method = graph.ShortestPathsDijkstra((x) => 1, start);
            var found = method.Invoke(end, out var paths);

            return paths?.Count() ?? -1;
        }

        private AdjacencyGraph<int, Edge<int>> BuildGraph(IEnumerable<string> input)
        {
            var matrix = input.Select(x => x.Replace('S', 'a').Replace('E', 'z').ToArray()).ToArray();
            var graph = new AdjacencyGraph<int, Edge<int>>();
            int w = input.First().Length;
            int h = input.Count();
            Enumerable.Range(0, w * h).ForEach(x => graph.AddVertex(x));

            var directions = new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var currentIndex = i * w + j;
                    var level = matrix[i][j];

                    foreach ((var di, var dj) in directions)
                    {
                        var nexti = i + di;
                        var nextj = j + dj;

                        if ((nexti, nextj).Within(matrix) && matrix[nexti][nextj] - level <= 1)
                            graph.AddEdge(new Edge<int>(currentIndex, nexti * w + nextj));
                    }
                }
            }

            return graph;
        }
    }
}
