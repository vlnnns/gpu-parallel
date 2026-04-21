using System.Text;
using static System.Console;

namespace Lab1_Var6;

public class FileDisplayTask
{
    private static readonly object _consoleLock = new();

    private readonly string _filePath;
    private readonly Random _rnd;

    public FileDisplayTask(string filePath, int seed)
    {
        _filePath = filePath;
        _rnd = new Random(seed);
    }

    public void Run()
    {
        string[] lines = File.ReadAllLines(_filePath, Encoding.UTF8);
        string fileName = Path.GetFileName(_filePath);

        foreach (string line in lines)
        {
            string output = $"[{fileName}] {line}";
            int maxLen = Math.Max(1, WindowWidth - 1);
            if (output.Length > maxLen) output = output.Substring(0, maxLen);

            int left = _rnd.Next(0, Math.Max(1, WindowWidth - output.Length));
            int top = _rnd.Next(0, Math.Max(1, WindowHeight - 1));

            try
            {
                lock (_consoleLock)
                {
                    SetCursorPosition(left, top);
                    WriteLine(output);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            Task.Delay(_rnd.Next(200, 800)).Wait();
        }
    }
}
