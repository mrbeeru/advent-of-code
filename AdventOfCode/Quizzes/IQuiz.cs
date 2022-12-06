using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Quizzes
{
    internal interface IQuiz
    {

    }

    internal interface IQuizPartOne<T> : IQuiz
    {
        T Part1();
    }

    internal interface IQuizPartTwo<T> : IQuiz
    {
        T Part2();
    }
}
