using AdventOfCode.Reader;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/7
    /// </summary>
    [Aoc(year: 2022, day: 7)]
    public class Day07(IInputProvider inputProvider) : IPartOne<long>, IPartTwo<long>
    {
        public long Part1()
        {
            var dirs = Parse(inputProvider.GetInput());
            return dirs.Where(x => x.TotalSize <= 100_000).Sum(x => x.TotalSize);
        }

        public long Part2()
        {
            var dirs = Parse(inputProvider.GetInput());
            var root = dirs.Single(x => x.Parent == null);

            // 30kk - space needed for update, 70kk total system space
            var spaceToFree = 30_000_000 - (70_000_000 - root.TotalSize);

            return dirs.Where(x => x.TotalSize > spaceToFree).OrderBy(x => x.TotalSize).First().TotalSize;
        }

        private IEnumerable<AocDirectory> Parse(IEnumerable<string> input)
        {
            var currentDir = new AocDirectory() { Name = "/" };
            var dirs = new List<AocDirectory>() { currentDir };

            foreach (var cmd in input)
            {
                if (cmd == "$ ls")
                    continue;
                else if (cmd.StartsWith("dir")) // a new directory
                {
                    var dir = new AocDirectory() { Name = cmd.Split(" ")[1] };
                    currentDir.AddChild(dir);
                    dirs.Add(dir);
                }
                else if (char.IsDigit(cmd[0]))  // a file
                    currentDir.Size += int.Parse(cmd.Split(" ")[0]);
                else if (cmd[5] == '.')         // cd .. command
                    currentDir = currentDir.Parent;
                else if (char.IsLetter(cmd[5])) // cd <dir> command
                    currentDir = currentDir.GetChildDirectory(cmd.Split(" ")[2]);
            }

            return dirs;
        }

        private class AocDirectory
        {
            private List<AocDirectory> dirs = new();

            public string? Name { get; set; }
            public AocDirectory Parent { get; set; }
            public long Size { get; set; }
            public long TotalSize => Size + dirs.Select(x => x.TotalSize).Sum();

            public void AddChild(AocDirectory dir) { dir.Parent = this; dirs.Add(dir); }
            public AocDirectory GetChildDirectory(string name) => dirs.First(x => x.Name == name);
        }
    }
}
