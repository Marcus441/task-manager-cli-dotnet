namespace TaskManagerCli.Services.Tui.Core;

public class Terminal
{
    public int Cols { get; private set; }
    public int Rows { get; private set; }
    public int Size { get; private set; }

    public Terminal()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            Console.CursorVisible = true;
            Console.Clear();
            Environment.Exit(0);
        };
        UpdateDimensions();
    }

    public bool UpdateDimensions()
    {
        if (Cols == Console.WindowWidth && Rows == Console.WindowHeight)
        {
            return false;
        }

        Cols = Console.WindowWidth;
        Rows = Console.WindowHeight;
        Size = Cols * Rows;
        return true;
    }
}
