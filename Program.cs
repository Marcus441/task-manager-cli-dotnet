using TaskManagerCli.src.Models;
using TaskManagerCli.src.Services;

var pman = new ProcessManager();
var procDict = pman.GetKeyValuePairs();
foreach (var proc in procDict)
{
    Console.WriteLine($"{proc.Key}: {proc.Value}");
}

