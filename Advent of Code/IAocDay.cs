using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal interface IAocDay<T>
    {
        T Part1();
        T Part2();
    }
}
