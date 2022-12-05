using AdventOfCode;
using AdventOfCode.Reader;
using AdventOfCode.Quizzes.Y2022;
using CommandLine;
using System.Reflection;

//Resolver.DisplayImplementedQuizzes();

Parser.Default.ParseArguments<Options, ShowVerb>(args)
    .WithParsed<Options>((opt) => Run(opt))
    .WithParsed<ShowVerb>((opt) => Show(opt));


void Run(Options opt)
{
    var fileInputReader = new FileInputReader(opt.FilePath ?? $"Quizzes/Y{opt.Year}/input{opt.Day:00}.txt");
    var (instance, method) = Resolver.Resolve(opt.Year, opt.Day, opt.Part, fileInputReader);
    var result = method.Invoke(instance, null);

    Console.WriteLine(result);
}

void Show(ShowVerb opt)
{
    Resolver.DisplayAvailableQuizzes();
}