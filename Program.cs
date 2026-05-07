using System.Diagnostics;
using TaskManagerCli.src.Services;

var sw = Stopwatch.StartNew();
var pman = await ProcessManager.CreateAsync();

var procDict = pman.GetKeyValuePairs();
foreach (var proc in procDict)
{
    Console.WriteLine($"{proc.Key}: {proc.Value}");
}
Console.WriteLine(sw.ElapsedMilliseconds);

