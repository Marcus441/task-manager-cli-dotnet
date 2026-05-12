namespace TaskManagerCli.Models;

public struct Cell
{
    public char Glyph { get; set; }
    public byte Background { get; set; }
    public byte Foreground { get; set; }
    public override readonly string ToString()
    {
        return $"\x1b[38;5;{Foreground}m\x1b[48;5;{Background}m{Glyph}\x1b[0m";
    }
};

