namespace TaskManagerCli.Models;

public struct Cell
{
    public char Glyph { get; set; }
    public byte Background { get; set; }
    public byte Foreground { get; set; }
    public override readonly string ToString()
    {
        return Glyph.ToString();
    }
};

