using TaskManagerCli.src.Services;

var pman = await ProcessManager.CreateAsync();
var runCount = 0;

while (true)
{
    await Task.Delay(100);

    await pman.RefreshAsync();
    var procDict = pman.GetKeyValuePairs();
    runCount++;

    foreach (var proc in procDict)
    {
        Console.WriteLine($"{proc.Key}: {proc.Value}");
    }

    Console.WriteLine(runCount);
}
