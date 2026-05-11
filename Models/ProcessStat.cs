namespace TaskManagerCli.Models;

public record ProcessStat(
    int Pid,
    int PPid,
    string Name,
    string State
)
{
    private static string Col(string value, int width) =>
        value.Length > width ? value[..(width - 1)] + "…" : value.PadRight(width);
    public override string ToString() =>
        $"{Col(Pid.ToString(), 9)}{Col(Name, 40)}{Col(State, 1)}";
}
