namespace TaskManagerCli.Services.Tui.Interfaces;

public interface IScrollable
{
    int ScrollOffset { get; }
    void ScrollUp(int lines = 1);
    void ScrollDown(int lines = 1);
}
