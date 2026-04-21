using System.Diagnostics;

namespace Lab2_Var6;

public static class CalculatorLauncher
{
    public static (Process Proc, ManualResetEventSlim Done) Launch(int index)
    {
        var beforeIds = Process.GetProcessesByName("Calculator")
                              .Select(p => p.Id)
                              .ToHashSet();

        var startInfo = new ProcessStartInfo
        {
            FileName = "/usr/bin/open",
            Arguments = "-n -a Calculator",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        using var opener = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Не вдалося запустити open");
        opener.WaitForExit();

        Process? calc = null;
        for (int retry = 0; retry < 30; retry++)
        {
            var current = Process.GetProcessesByName("Calculator");
            calc = current.FirstOrDefault(p => !beforeIds.Contains(p.Id));
            if (calc != null) break;
            Thread.Sleep(100);
        }

        if (calc == null)
            throw new InvalidOperationException(
                $"Не вдалося знайти новий процес Calculator для index={index}");

        var done = new ManualResetEventSlim(false);
        calc.EnableRaisingEvents = true;
        int humanIndex = index + 1;
        calc.Exited += (_, _) =>
        {
            Console.WriteLine($"{humanIndex}-ий Калькулятор закритий");
            done.Set();
        };

        return (calc, done);
    }
}
