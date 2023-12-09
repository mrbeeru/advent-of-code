using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using QuikGraph;
using QuikGraph.Algorithms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static MoreLinq.Extensions.ForEachExtension;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day16 : IPartOne<long>
    {
        readonly IInputProvider inputProvider;
        Dictionary<string, Valve> map = new();
        Dictionary<(string, string), int> lengths = new();
        AdjacencyGraph<string, Edge<string>> graph;
        List<Valve> valveList = new();
        int maxScore = 0;

        public Day16(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            (var root, graph, map) = Parse();
            valveList = map.Select(x => x.Value).Where(x => x.Pressure != 0 || x.ID == "AA").ToList();
            ComputeLengths();
            Search(new List<Valve> { map["AA"] }, 0, 0, maxTime: 30);
            return maxScore;
        }

        private void Search(List<Valve> result, int totalLength, int score, int maxTime)
        {
            maxScore = Math.Max(score, maxScore);
            foreach (var a in valveList.Except(result))
            {
                var length = lengths[(result.Last().ID, a.ID)];

                if (totalLength + length < maxTime)
                {
                    result.Add(a);
                    Search(result, totalLength + length + 1, score +  (maxTime - totalLength - length - 1) * a.Pressure, maxTime);
                    maxScore = Math.Max(score, maxScore);
                    result.Remove(a);
                }

                //if (result.Count == 2)
                //Console.WriteLine("yey");
            }
        }

        private void ComputeLengths()
        {
            for (int i = 0; i < valveList.Count; i++)
            {
                for (int j = 0; j < valveList.Count; j++)
                {
                    if (i == j)
                        continue;

                    var len = Length(valveList[i].ID, valveList[j].ID);
                    lengths[(valveList[i].ID, valveList[j].ID)] = len;
                }
            }
        }

        private int Length(string start, string end)
        {
            var method = graph.ShortestPathsDijkstra((x) => 1, start);
            var success = method.Invoke(end, out var result);

            return result.Count();
        }

        private (Valve, AdjacencyGraph<string, Edge<string>>, Dictionary<string, Valve>) Parse()
        {
            var input = inputProvider.GetInput();
            var graph = new AdjacencyGraph<string, Edge<string>>();

            input.ForEach(x =>
            {
                map.Add(x[6..8],
                    new Valve
                    {
                        ID = x[6..8],
                        Pressure = x.Nums().First()
                    });
                graph.AddVertex(x[6..8]);
            });

            foreach (var line in input)
            {
                var matches = Regex.Matches(line, @"[A-Z]{2}");
                var valve = map[matches.First().Value];
                matches.Skip(1).ForEach(x =>
                {
                    valve.Next.Add(map[x.Value]);
                    graph.AddEdge(new Edge<string>(valve.ID, x.Value));
                });
            }

            return (map["AA"], graph, map);
        }

        [DebuggerDisplay("{ID} : {Pressure}")]
        private class Valve
        {
            public string ID { get; set; }
            public int Pressure { get; set; }
            public bool IsOpen { get; set; }
            public List<Valve> Next { get; set; } = new();
        }
    }
}
