namespace TaskManagerCli.Services.Tui;

using TaskManagerCli.Services.Tui.Core;
using TaskManagerCli.Services.Tui.Interfaces;
using TaskManagerCli.Models;

public class Screen : ICanvas
{
    private readonly Terminal _terminal;
    public int Height => _terminal.Rows;
    public int Width => _terminal.Cols;
    private Cell[] _backBuff = null!;
    private Cell[] _frontBuff = null!;

    public Screen(Terminal terminal)
    {
        _terminal = terminal;
        ResizeBuffers();
    }

    public void ResizeBuffers()
    {
        _terminal.UpdateDimensions();

        _backBuff = new Cell[_terminal.Size];
        _frontBuff = new Cell[_terminal.Size];
    }

    public void Render()
    {
        for (var i = 0; i < _terminal.Size; i++)
        {
            if (!_backBuff[i].Equals(_frontBuff[i]))
            {
                _frontBuff[i] = _backBuff[i];
                var col = i % _terminal.Cols;
                var row = i / _terminal.Cols;
                // point cursor to position
                var cursorCommand = $"\x1b[{row};{col}H";
                // print _backBuff[i].Glyph with bg and fg
                Console.Write(cursorCommand + _backBuff[i].ToString());

                // sync
                _frontBuff[i] = _backBuff[i];
            }
        }
    }
    public void Clear()
    {
        Array.Fill(_backBuff, new Cell { Glyph = ' ', Foreground = 7, Background = 0 });
    }
    public void DrawString(int x, int y, ReadOnlySpan<char> text, byte fg = 7 /* white */, byte bg = 0 /* black */)
    {
        var startIndex = y * _terminal.Cols + x;

        for (var i = 0; i < text.Length; i++)
        {
            var index = startIndex + i;

            if (index >= 0 && index < _backBuff.Length)
            {
                _backBuff[index] = new Cell { Glyph = text[i], Foreground = fg, Background = bg };
            }
        }
    }

}
