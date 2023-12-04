using AdventOfCode;
using AdventOfCode.Reader;
using AdventOfCode.Quizzes.Y2022;
using CommandLine;
using System.Reflection;
using System.Diagnostics;


Parser.Default.ParseArguments<Options, ShowVerb>(args)
    .WithParsed<Options>(Run)
    .WithParsed<ShowVerb>(Show);

void Run(Options opt)
{
    var httpInputReader = new HttpInputReader(opt.Year, opt.Day);
    //do this to cache
    httpInputReader.GetInput();
    var (instance, method) = Resolver.Resolve(opt.Year, opt.Day, opt.Part, httpInputReader);

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