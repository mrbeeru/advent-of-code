using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Reader
{
    internal class FileInputReader : IInputProvider
    {
        private string filePath;

        public FileInputReader(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<string> GetInput()
        {
            return File.ReadAllLines(filePath);
        }
    }
}
