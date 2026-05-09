namespace TaskManagerCli.src.Services;

using TaskManagerCli.src.Models;

public class Screen
{
    private readonly Terminal _terminal;
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
                // print _backBuff[i].Glyph with bg and fg

                // sync
                _frontBuff[i] = _backBuff[i];
            }
        }
    }

}
