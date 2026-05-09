using TaskManagerCli.src.Models;

namespace TaskManagerCli.src.Services;

public class ProcessManager
{
    private readonly Dictionary<int, ProcessStat> _byPid = [];
    private readonly Dictionary<int, List<int>> _children = [];

    public ProcessStat? GetProcess(int pid)
        => _byPid.GetValueOrDefault(pid);
    public Dictionary<int, ProcessStat> GetKeyValuePairs() => _byPid;

    public static async Task<ProcessManager> CreateAsync()
    {
        var pm = new ProcessManager();
        await pm.PopulateProcessPidDictAsync();
        return pm;
    }

    public async Task RefreshAsync()
    {
        _byPid.Clear();
        _children.Clear();
        await PopulateProcessPidDictAsync();
    }

    private static string ProcStatDir(int pid) => $"/proc/{pid}/stat";
    private static async Task<string?> ReadProcAsync(int pid)
    {
        try
        {
            using StreamReader procStatFile = new(ProcStatDir(pid));
            return await procStatFile.ReadToEndAsync();
        }
        catch (Exception e) when (e is FileNotFoundException or IOException)
        {
            return null;
        }
    }
    private async Task PopulateProcessPidDictAsync()
    {
        var tasks = Directory.EnumerateDirectories("/proc")
            .Select(dir =>
            {
                var isValid = int.TryParse(Path.GetFileName(dir), out var pid);
                return (isValid, pid);
            })
            .Where(x => x.isValid)
            .Select(x => ReadProcAsync(x.pid))
            .Select(async statString =>
            {
                var result = await statString;
                if (result is not null)
                    return StatParser.Parse(result);
                return null;
            });

        ProcessStat?[] results = await Task.WhenAll(tasks);

        foreach (var proc in results)
        {
            if (proc == null)
                continue;
            _byPid[proc.Pid] = proc;

            if (!_children.ContainsKey(proc.PPid))
                _children[proc.PPid] = [];
            _children[proc.PPid].Add(proc.Pid);
        }
    }

}

