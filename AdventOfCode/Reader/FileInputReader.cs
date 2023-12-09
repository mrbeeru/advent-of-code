namespace AdventOfCode.Reader
{
    internal class FileInputReader : IInputProvider
    {
        private string filePath;

        public FileInputReader(string filePath)
        {
            this.filePath = filePath;
        }

        public IList<string> GetInput()
        {
            return File.ReadAllLines(filePath);
        }
    }
}
