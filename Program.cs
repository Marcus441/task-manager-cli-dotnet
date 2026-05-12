using TaskManagerCli.Services;
using TaskManagerCli.Services.Tui;
using TaskManagerCli.Services.Tui.Components;
using TaskManagerCli.Services.Tui.Core;

var pman = await ProcessManager.CreateAsync();
var terminal = new Terminal();
var canvas = new Screen(terminal);
var taskList = new TaskListView(pman.Processes, canvas.Height);
var running = true;

while (running)
{
    await pman.RefreshAsync();
    while (Console.KeyAvailable)
    {
        var key = Console.ReadKey(intercept: true);
        switch (key.KeyChar)
        {
            case 'q':
                running = false;
                break;
            case 'j':
                taskList.MoveDown();
                break;
            case 'k':
                taskList.MoveUp();
                break;

        }
    }

    canvas.Clear();
    taskList.Draw(canvas);
    canvas.Render();
    await Task.Delay(16);
}
