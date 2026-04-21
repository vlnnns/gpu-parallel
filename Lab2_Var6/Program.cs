using System.Diagnostics;
using System.Text;
using Lab2_Var6;
using static System.Console;

OutputEncoding = Encoding.UTF8;

Write("Скільки Калькуляторів запустити? N = ");
string input = ReadLine() ?? "";
if (!int.TryParse(input, out int n) || n <= 0)
{
    WriteLine("Невірне N. Введіть додатне ціле число.");
    return;
}

WriteLine($"Запускаємо {n} процесів Калькулятора...");

var processes = new Process[n];
var doneEvents = new ManualResetEventSlim[n];
for (int i = 0; i < n; i++)
{
    var (proc, done) = CalculatorLauncher.Launch(i);
    processes[i] = proc;
    doneEvents[i] = done;
    WriteLine($"{i + 1}-ий Калькулятор запущений (PID={proc.Id})");
}

WriteLine();
WriteLine("Закрийте вікна Калькулятора — програма чекає завершення всіх процесів.");
WriteLine();

foreach (var done in doneEvents)
{
    done.Wait();
}

WriteLine();
WriteLine("Усі Калькулятори закриті. Завершення програми.");
