using System.Text;
using Lab1_Var6;
using static System.Console;

OutputEncoding = Encoding.UTF8;

Write("Вкажіть шлях до папки з .txt файлами (Enter = TestFiles): ");
string input = ReadLine() ?? "";
string folder = string.IsNullOrWhiteSpace(input)
    ? Path.Combine(AppContext.BaseDirectory, "TestFiles")
    : input;

if (!Directory.Exists(folder))
{
    WriteLine($"Папку не знайдено: {folder}");
    return;
}

string[] files = Directory.GetFiles(folder, "*.txt");
WriteLine($"Знайдено файлів: {files.Length}");

if (files.Length == 0)
{
    WriteLine("Немає .txt файлів. Завершення.");
    return;
}

WriteLine("Натисніть Enter для старту...");
ReadLine();
Clear();

var tasks = new Task[files.Length];
for (int i = 0; i < files.Length; i++)
{
    var worker = new FileDisplayTask(files[i], seed: i);
    tasks[i] = new Task(action: worker.Run);
}

foreach (var t in tasks) t.Start();

Task.WaitAll(tasks);

SetCursorPosition(0, Math.Max(0, WindowHeight - 2));
WriteLine("Усі задачі завершено. Натисніть Enter.");
ReadLine();
