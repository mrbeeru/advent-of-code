namespace AdventOfCode.Quizzes
{
    internal interface IQuiz
    {

    }

    internal interface IPartOne<T> : IQuiz
    {
        T Part1();
    }

    internal interface IPartTwo<T> : IQuiz
    {
        T Part2();
    }
}
