using AdventOfCode.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Instantiates the correspoding AOC day class and method.
    /// </summary>
    internal class Resolver
    {
        public static (object?, MethodInfo) Resolve(int year, int day, int part, IInputProvider provider)
        {
            if (part != 1 && part != 2)
                throw new Exception("Part can be 1 or 2");

            var cls = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Single(x => x.FullName == $"AdventOfCode.Y{year}.Day{day:00}");

            if (cls == null)
                throw new Exception($"AOC {year}-{day:00} is not implemented.");

            var method = cls.GetMethod($"Part{part}");

            if (method == null)
                throw new Exception($"AOC {year}-{day:00} Part {part} is not implemented.");

            var instance = Activator.CreateInstance(cls, new object[] { provider });

            return (instance, method);
        }
    }
}
