namespace TaskManagerCli.Services.Tui.Interfaces;

public interface ICanvas
{
    int Height { get; }
    int Width { get; }
    void Clear();
    public void DrawString(int x, int y, ReadOnlySpan<char> text, byte fg = 7 /* white */, byte bg = 0 /* black */);
}
