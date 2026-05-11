using TaskManagerCli.Services;
using TaskManagerCli.Services.Tui;
using TaskManagerCli.Services.Tui.Components;
using TaskManagerCli.Services.Tui.Core;

var pman = await ProcessManager.CreateAsync();
var terminal = new Terminal();
var canvas = new Screen(terminal);
var taskList = new TaskListView(pman.Processes); // direct reference

while (true)
{
    await Task.Delay(1000);
    await pman.RefreshAsync();
    taskList.Draw(canvas);
    canvas.Render();
}
