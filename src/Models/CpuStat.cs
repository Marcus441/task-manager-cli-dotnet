namespace TaskManagerCli.src.Models;

public record CpuStat(
     int User = default,
     int Nice = default,
     int System = default,
     int Idle = default,
     int IoWait = default,
     int Irq = default,
     int SoftIrq = default,
     int Steal = default,
     int Guest = default,
     int GuestNice = default
)
{
    public static CpuStat? Read()
    {
        try
        {
            using StreamReader reader = new("/proc/stat");
            string text = reader.ReadToEnd();
            int start = text.IndexOf("cpu");
            int end = text.IndexOf('\n', start);
            string cpuLine = text[start..end];
            int[] cpuLineInts = [.. cpuLine
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
            ];
            return new CpuStat(
                cpuLineInts[0],
                cpuLineInts[1],
                cpuLineInts[2],
                cpuLineInts[3],
                cpuLineInts[4],
                cpuLineInts[5],
                cpuLineInts[6],
                cpuLineInts[7],
                cpuLineInts[8],
                cpuLineInts[9]);

        }
        catch (IOException e)
        {
            Console.WriteLine("Could not read /proc/stat");
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static CpuStat? ReadCore(int cpuCore)
    {
        try
        {
            using StreamReader reader = new("/proc/stat");
            string text = reader.ReadToEnd();
            int start = text.IndexOf($"cpu{cpuCore}");
            int end = text.IndexOf('\n', start);
            string cpuLine = text[start..end];
            int[] cpuLineInts = [.. cpuLine
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
            ];
            return new CpuStat(
                cpuLineInts[0],
                cpuLineInts[1],
                cpuLineInts[2],
                cpuLineInts[3],
                cpuLineInts[4],
                cpuLineInts[5],
                cpuLineInts[6],
                cpuLineInts[7],
                cpuLineInts[8],
                cpuLineInts[9]);

        }
        catch (IOException e)
        {
            Console.WriteLine("Could not read /proc/stat");
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
