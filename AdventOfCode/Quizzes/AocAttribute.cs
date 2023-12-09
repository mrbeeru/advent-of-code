namespace AdventOfCode.Quizzes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AocAttribute(int year, int day) : Attribute
    {
        public int Year => year;
        public int Day => day;
    }
}
