using TaskManagerCli.Models;
using TaskManagerCli.Services;

var pman = await ProcessManager.CreateAsync();
// var runCount = 0;
var terminal = new Terminal();
var screen = new Screen(terminal);

while (true)
{
    await Task.Delay(100);

    await pman.RefreshAsync();
    var procDict = pman.GetKeyValuePairs();
    // runCount++;

    var row = 0;
    foreach (var proc in procDict)
    {
        screen.DrawString(0, row, $"{proc.Key}: {proc.Value}".AsSpan());
        row++;

        if (row >= terminal.Rows)
        {
            break;
        }
    }
    screen.Render();

    // Console.WriteLine(runCount);
}
