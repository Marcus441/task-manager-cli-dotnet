using TaskManagerCli.src.Models;

namespace TaskManagerCli.src.Services;

public class ProcessManager
{
    private readonly Dictionary<int, ProcessStat> _byPid = [];
    private readonly Dictionary<int, List<int>> _children = [];

    public ProcessStat? GetProcess(int pid)
        => _byPid.GetValueOrDefault(pid);
    public Dictionary<int, ProcessStat> GetKeyValuePairs() => _byPid;
    public ProcessManager()
    {
        PopulateProcessPidDict();
    }
    public void Refresh()
    {
        _byPid.Clear();
        _children.Clear();
        PopulateProcessPidDict();
    }

    private static string ProcStatDir(int pid) => $"/proc/{pid}/stat";
    private static ProcessStat? ReadProc(int pid)
    {
        try
        {
            using StreamReader procStatFile = new(ProcStatDir(pid));
            return StatParser.Parse(procStatFile.ReadToEnd());
        }
        catch (Exception e) when (e is FileNotFoundException or IOException)
        {
            return null;
        }
    }
    private void PopulateProcessPidDict()
    {
        foreach (var dir in Directory.EnumerateDirectories("/proc"))
        {
            if (!int.TryParse(Path.GetFileName(dir), out int pid))
                continue;
            var proc = ReadProc(pid);
            if (proc is null) continue;
            _byPid[proc.Pid] = proc;

            if (!_children.ContainsKey(proc.PPid))
                _children[proc.PPid] = [];
            _children[proc.PPid].Add(proc.Pid);
        }
    }

}

