using TaskManagerCli.src.Models;

namespace TaskManagerCli.src.Services;

public class StatParser
{
    private enum StatField
    {
        Pid = 0,
        Comm = 1,       // process name, wrapped in parens
        State = 2,
        PPid = 3,
    }
    private static string[] BuildFields(string statLine)
    {

        var commStart = statLine.IndexOf('(');
        var commEnd = statLine.LastIndexOf(')');
        var name = statLine[(commStart + 1)..commEnd];
        var pid = statLine[..commStart].Trim();

        return [pid, name, .. statLine[(commEnd + 2)..].Split(' ')];
    }
    public static ProcessStat Parse(string statLine)
    {
        var fields = BuildFields(statLine);
        Console.WriteLine(string.Join(", ", fields));
        return new ProcessStat(
            Pid: int.Parse(fields[(int)StatField.Pid]),
            PPid: int.Parse(fields[(int)StatField.PPid]),
            Name: fields[(int)StatField.Comm],
            State: fields[(int)StatField.State]
        );
    }
}
