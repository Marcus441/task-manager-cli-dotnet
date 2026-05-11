namespace TaskManagerCli.Services.Tui.Components;

using TaskManagerCli.Models;
using TaskManagerCli.Services.Tui.Interfaces;

public class TaskListView(IReadOnlyDictionary<int, ProcessStat> processes) : IScrollable
{
    private readonly IReadOnlyDictionary<int, ProcessStat> _processes = processes;

    public int ScrollOffset { get; private set; }
    public void ScrollUp(int lines = 1) => ScrollOffset = Math.Max(0, ScrollOffset - lines);
    public void ScrollDown(int lines = 1) => ScrollOffset += lines;

    public void Draw(ICanvas canvas)
    {
        var visible = _processes.Values
            .OrderBy(p => p.Pid)
            .Skip(ScrollOffset)
            .Take(canvas.Height);

        var row = 0;
        foreach (var proc in visible)
        {
            canvas.DrawString(0, row++, proc.ToString());
        }
    }
}
