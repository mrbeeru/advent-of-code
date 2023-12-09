using AdventOfCode.Reader;
using MoreLinq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Quizzes.Y2022
{
    [Aoc(year: 2022, day: 19)]
    public class Day19(IInputProvider inputProvider) : IPartOne<long>
    {
        private int maxTime = 24;
        HashSet<((int, int, int, int), (int, int, int, int), int)> Memoization = new();
        int[] scores;

        public long Part1()
        {
            var input = inputProvider.GetInput();
            var blueprints = Parse(input);
            scores = new int[blueprints.Count + 1];

            foreach (var blueprint in blueprints)
            {
                int score = 0;
                EvaluateBlueprint(blueprint, (0, 0, 0, 0), (1, 0, 0, 0), 1, ref score);
                Memoization.Clear();
                scores[blueprint.ID] = score;
                Console.WriteLine($"Blueprint {blueprint.ID} | Score {scores[blueprint.ID]}");
            }

            return scores.Select((x, i) => i * scores[i]).Sum();
        }

        private void EvaluateBlueprint(
            Blueprint blueprint,
            (int ore, int clay, int obs, int geo) totalProduction,
            (int ore, int clay, int obs, int geo) currentProduction,
            int minute,
            ref int maxScore)
        {
            if (minute > maxTime)
                return;

            var otherRobots = GetAvailableRobots(totalProduction, blueprint);
            totalProduction = Sum(totalProduction, currentProduction);

            if (totalProduction.geo > maxScore)
            {
                maxScore = totalProduction.geo;
            }

            if (Memoization.Contains((totalProduction, currentProduction, minute)))
                return;

            //check to see if we can build another robot
            for (int i = 0; i < otherRobots.Count; i++)
            {
                var r = otherRobots[i];
                totalProduction = Diff(totalProduction, r.Cost);
                currentProduction = Sum(currentProduction, r.Produce);
                EvaluateBlueprint(blueprint, totalProduction, currentProduction, minute + 1, ref maxScore);
                Memoization.Add((totalProduction, currentProduction, minute));
                currentProduction = Diff(currentProduction, r.Produce);
                totalProduction = Sum(totalProduction, r.Cost);
            }

            EvaluateBlueprint(blueprint, totalProduction, currentProduction, minute + 1, ref maxScore);
        }

        private (int ore, int clay, int obs, int geo) Sum((int, int, int, int) a, (int, int, int, int) b)
        {
            return (a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3, a.Item4 + b.Item4);
        }

        private (int ore, int clay, int obs, int geo) Diff((int, int, int, int) a, (int, int, int, int) b)
        {
            return (a.Item1 - b.Item1, a.Item2 - b.Item2, a.Item3 - b.Item3, a.Item4 - b.Item4);
        }

        private List<Robot> GetAvailableRobots((int ore, int clay, int obs, int geo) materials, Blueprint blueprint)
        {
            var output = new List<Robot>();

            foreach (var robot in blueprint.Robots)
            {
                if (robot.CanBuild(materials))
                    output.Add(robot);
            }

            return output;
        }

        private List<Blueprint> Parse(IEnumerable<string> input)
        {
            var blueprints = new List<Blueprint>();

            foreach (var line in input)
            {
                var blueprint = new Blueprint();
                blueprint.ID = int.Parse(Regex.Match(line, @"Blueprint (\d+):").Groups[1].Value);

                line[line.IndexOf(':')..]
                    .Split('.', StringSplitOptions.RemoveEmptyEntries)
                    .ForEach(x => blueprint.Robots.Add(Robot.Parse(x)));

                blueprints.Add(blueprint);
            }

            return blueprints;
        }

        private class Round
        {

        }

        private class Blueprint
        {
            public int ID { get; set; }
            public List<Robot> Robots = new() { };
        }

        [DebuggerDisplay("{Kind}")]
        private class Robot : IEquatable<Robot>
        {
            public enum RobotKind
            {
                None,
                Ore,
                Clay,
                Obsidian,
                Geode,
            }

            public RobotKind Kind { get; set; }
            public (int ore, int clay, int obs, int geo) Cost { get; set; }
            public (int ore, int clay, int obs, int geo) Produce { get; set; }

            public static Robot Parse(string line)
            {
                Robot robot = new Robot();
                var matches = Regex.Match(line, @"Each (\w+) robot costs (\d+) (\w+)(?: and )?(?:(\d+) (\w+))?");

                robot.Kind = matches.Groups[1].Value switch
                {
                    "ore" => RobotKind.Ore,
                    "clay" => RobotKind.Clay,
                    "obsidian" => RobotKind.Obsidian,
                    "geode" => RobotKind.Geode,
                    _ => throw new Exception("Invalid robot type.")
                };

                robot.Produce = robot.Kind switch
                {
                    RobotKind.Ore => (1, 0, 0, 0),
                    RobotKind.Clay => (0, 1, 0, 0),
                    RobotKind.Obsidian => (0, 0, 1, 0),
                    RobotKind.Geode => (0, 0, 0, 1),
                    _ => throw new Exception("Invalid robot type.")
                };

                var result = int.TryParse(matches.Groups[2].Value, out int cost);
                var costType = matches.Groups[3].Value;

                if (result)
                {
                    if (costType == "ore")
                        robot.Cost = (robot.Cost.ore + cost, robot.Cost.clay, robot.Cost.obs, 0);
                    else if (costType == "clay")
                        robot.Cost = (robot.Cost.ore, robot.Cost.clay + cost, robot.Cost.obs, 0);
                    else if (costType == "obsidian")
                        robot.Cost = (robot.Cost.ore, robot.Cost.clay, robot.Cost.obs + cost, 0);
                }

                result = int.TryParse(matches.Groups[4].Value, out cost);
                costType = matches.Groups[5].Value;

                if (result)
                {
                    if (costType == "ore")
                        robot.Cost = (robot.Cost.ore + cost, robot.Cost.clay, robot.Cost.obs, 0);
                    else if (costType == "clay")
                        robot.Cost = (robot.Cost.ore, robot.Cost.clay + cost, robot.Cost.obs, 0);
                    else if (costType == "obsidian")
                        robot.Cost = (robot.Cost.ore, robot.Cost.clay, robot.Cost.obs + cost, 0);
                }

                return robot;
            }

            public bool CanBuild((int ore, int clay, int obs, int geo) mats)
            {
                return mats.ore >= Cost.ore && mats.clay >= Cost.clay && mats.obs >= Cost.obs;
            }

            public bool Equals(Robot? other)
            {
                return Kind == other.Kind;
            }

            private static Robot none;
            public static Robot None => none ??= new Robot() { Kind = RobotKind.None };
        }
    }
}
