﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Reader
{
    public interface IInputProvider
    {
        IList<string> GetInput();
    }
}
