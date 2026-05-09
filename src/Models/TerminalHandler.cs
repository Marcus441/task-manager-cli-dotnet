namespace TaskManagerCli.src.Models;

public class Terminal
{
    public int Cols { get; private set; }
    public int Rows { get; private set; }
    public int Size { get; private set; }

    public Terminal()
    {
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
