namespace TaskManagerCli.src.Models;

public record ProcessStat(
    int Pid,
    int PPid,
    string Name,
    string State
// float CpuPercent,
// long MemoryKb,
// string? User = null
);
