using AdventOfCode;
using AdventOfCode.Reader;
using AdventOfCode.Y2022;
using CommandLine;
using System.Reflection;


Parser.Default.ParseArguments<Options>(args).WithParsed((opt) => Invoke(opt));


void Invoke(Options opt)
{
    if (opt.Part != 1 && opt.Part != 2)
        throw new Exception("Part can be 1 or 2");

    var cls = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Single(x => x.FullName == $"AdventOfCode.Y{opt.Year}.Day{opt.Day:00}");

    if (cls == null)
        throw new Exception($"AOC {opt.Year}-{opt.Day:00} is not implemented.");

    var method = cls.GetMethod($"Part{opt.Part}");

    if (method == null)
        throw new Exception($"AOC {opt.Year}-{opt.Day:00} Part {opt.Part} is not implemented.");

    var fileInputReader = new FileInputReader(opt.FilePath);
    
    var instance = Activator.CreateInstance(cls, new object[] { fileInputReader });
    var result = method.Invoke(instance, null);

    Console.WriteLine(result);
}
