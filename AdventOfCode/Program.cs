using AdventOfCode;
using AdventOfCode.Reader;
using AdventOfCode.Quizzes.Y2022;
using CommandLine;
using System.Reflection;
using System.Diagnostics;


Parser.Default.ParseArguments<Options, ShowVerb>(args)
    .WithParsed<Options>((opt) => Run(opt))
    .WithParsed<ShowVerb>((opt) => Show(opt));


void Run(Options opt)
{
    var fileInputReader = new FileInputReader(opt.FilePath ?? $"Quizzes/Y{opt.Year}/input{opt.Day:00}.txt");
    var (instance, method) = Resolver.Resolve(opt.Year, opt.Day, opt.Part, fileInputReader);

    var start = Stopwatch.GetTimestamp();
    var result = method.Invoke(instance, null);
    var end = Stopwatch.GetElapsedTime(start);

    Console.WriteLine(result);
    Console.WriteLine($"Ran for {end.TotalMilliseconds / 1000.0} seconds.");
}

void Show(ShowVerb opt)
{
    Resolver.DisplayAvailableQuizzes();
}