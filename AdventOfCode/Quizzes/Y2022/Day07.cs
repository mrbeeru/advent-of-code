using AdventOfCode.Reader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes.Y2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/7
    /// </summary>
    internal class Day07 : IQuizPartOne<long>, IQuizPartTwo<long>
    {
        private readonly IInputProvider inputProvider;

        public Day07(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        public long Part1()
        {
            var input = this.inputProvider.GetInput();
            var dirs = Parse(input);
           
            return dirs.Where(x => x.TotalSize <= 100_000).Sum(x => x.TotalSize);
        }

        public long Part2()
        {
            var input = this.inputProvider.GetInput();
            var dirs = Parse(input);
            var root = dirs.Single(x => x.Parent == null);

            var totalSystemSpace = 70_000_000;
            var spaceForUpdate = 30_000_000;
            var freeSpace = (totalSystemSpace - root.TotalSize);
            var spaceToFree = spaceForUpdate - freeSpace;

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
                else if (cmd.StartsWith("dir"))
                {
                    var dir = new AocDirectory() { Name = cmd.Split(" ")[1] };
                    currentDir.AddChild(dir);
                    dirs.Add(dir);
                }
                else if (char.IsDigit(cmd[0]))
                {
                    var file = new AocFile() { Size = int.Parse(cmd.Split(" ")[0]) };
                    currentDir.AddFile(file);
                }
                else if (cmd[5] == '.')
                    currentDir = currentDir.Parent;
                else if (char.IsLetter(cmd[5]))
                    currentDir = currentDir.GetChildDirectory(cmd.Split(" ")[2]);
            }

            return dirs;
        }

        private class AocDirectory
        {
            private List<AocFile> files = new();
            public List<AocDirectory> dirs = new();

            public string Name { get; set; }
            public AocDirectory Parent { get; set; }
            public long Size => files.Select(x => x.Size).Sum();
            public long TotalSize => Size + dirs.Select(x => x.TotalSize).Sum();


            public void AddFile(AocFile file)
            {
                files.Add(file);
            }

            public void AddChild(AocDirectory dir)
            {
                dir.Parent = this;
                dirs.Add(dir);
            }

            public AocDirectory GetChildDirectory(string name)
            {
                return dirs.First(x => x.Name == name);
            }
        }

        private class AocFile
        {
            public long Size { get; set; }
        }
    }
}
