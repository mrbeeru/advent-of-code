using AdventOfCode;
using AdventOfCode.Reader;
using AdventOfCode.Y2022;
using CommandLine;
using System.Reflection;


Parser.Default.ParseArguments<Options>(args).WithParsed((opt) => Invoke(opt));


void Invoke(Options opt)
{
    var fileInputReader = new FileInputReader(opt.FilePath);
    var (instance, method) = Resolver.Resolve(opt.Year, opt.Day, opt.Part, fileInputReader);
    var result = method.Invoke(instance, null);

    Console.WriteLine(result);
}
