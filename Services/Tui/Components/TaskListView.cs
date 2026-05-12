namespace TaskManagerCli.Services.Tui.Components;

using TaskManagerCli.Models;
using TaskManagerCli.Services.Tui.Interfaces;

public class TaskListView(IReadOnlyDictionary<int, ProcessStat> processes, int maxHeight) : IScrollable
{
    private readonly int _maxHeight = maxHeight;
    private readonly IReadOnlyDictionary<int, ProcessStat> _processes = processes;
    private int _selectedAbsolute = 0;

    public int ScrollOffset { get; private set; }

    private int SelectedRelative => _selectedAbsolute - ScrollOffset;
    public void ScrollUp(int lines = 1)
    {
        ScrollOffset = Math.Max(0, ScrollOffset - lines);
    }
    public void ScrollDown(int lines = 1)
    {
        ScrollOffset = Math.Min(ScrollOffset + lines, _processes.Count - _maxHeight);
    }
    public void MoveUp()
    {
        if (_selectedAbsolute <= 0)
        {
            return;
        }
        _selectedAbsolute--;
        if (_selectedAbsolute < ScrollOffset)
        {
            ScrollUp();
        }
    }
    public void MoveDown()
    {
        if (_selectedAbsolute >= _processes.Count - 1)
        {
            return;
        }
        _selectedAbsolute++;
        if (_selectedAbsolute == ScrollOffset + _maxHeight)
        {
            ScrollDown();
        }
    }

    public void Draw(ICanvas canvas)
    {
        var visible = _processes.Values
            .OrderBy(p => p.Pid)
            .Skip(ScrollOffset)
            .Take(canvas.Height);

        var row = 0;
        foreach (var proc in visible)
        {
            if (row == SelectedRelative)
            {
                canvas.DrawString(0, row++, proc.ToString(), 0, 7);
            }
            else
            {
                canvas.DrawString(0, row++, proc.ToString());
            }
        }
    }
}
