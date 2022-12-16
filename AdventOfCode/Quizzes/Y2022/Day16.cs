using AdventOfCode.Extensions;
using AdventOfCode.Reader;
using MoreLinq;
using QuikGraph;
using QuikGraph.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    public class Day16 : IPartOne<long>
    {
        private readonly IInputProvider inputProvider;

        Dictionary<string, Valve> map = new();
        AdjacencyGraph<string, Edge<string>> graph;
        List<Valve> valveList = new();

        public Day16(IInputProvider inputProvider) => this.inputProvider = inputProvider;

        public long Part1()
        {
            (var root, graph, map) = Parse();
            valveList = map.Select(x => x.Value).Where(x => x.Pressure != 0).ToList();

            var score = 0;
            var len = 0;
            Search(new List<Valve> { map["AA"] }, len, score);

            return maxScore;
        }

        int maxScore = 0;
        private void Search(List<Valve> result, int totalLength, int score)
        {
            maxScore = Math.Max(score, maxScore);
            foreach (var a in valveList.Except(result))
            {
                var length = Length(result.Last().ID, a.ID);

                if (totalLength + length < 30)
                {
                    result.Add(a);
                    Search(result, totalLength + length + 1, score +  (30 - totalLength - length - 1) * a.Pressure);
                    maxScore = Math.Max(score, maxScore);
                    result.Remove(a);
                }

                if (result.Count == 1)
                    Console.WriteLine("yey");
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

            input.ForEach(x => {
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
                matches.Skip(1).ForEach(x => {
                    valve.Next.Add(map[x.Value]);
                    graph.AddEdge(new Edge<string>(valve.ID, x.Value));
                });
            }

            return (map["AA"], graph, map);
        }

        private AdjacencyGraph<string, Edge<string>> BuildGraph()
        {
            var graph = new AdjacencyGraph<string, Edge<string>>();



            return graph;
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
