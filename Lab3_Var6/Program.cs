using System.Text;
using Lab3_Var6;
using static System.Console;

OutputEncoding = Encoding.UTF8;

Write("Скільки чисел Фібоначчі згенерувати? N = ");
string input = ReadLine() ?? "";
if (!int.TryParse(input, out int n) || n <= 0)
{
    WriteLine("Невірне N. Введіть додатне ціле число.");
    return;
}

if (n > 90)
{
    WriteLine($"Увага: long переповниться при Fib({n}). Рекомендується N ≤ 90.");
    Write("Продовжити? (y/n): ");
    string answer = ReadLine() ?? "";
    if (answer.ToLower() != "y") return;
}

WriteLine($"Генеруємо {n} чисел Фібоначчі з інтервалом 1 сек...\n");

var buffer = new SharedBuffer();
var producer = new FibonacciProducer(buffer, n);
var consumer = new FibonacciConsumer(buffer, n);

var t1 = new Thread(producer.Run) { Name = "Producer" };
var t2 = new Thread(consumer.Run) { Name = "Consumer" };

t1.Start();
t2.Start();

t1.Join();
t2.Join();

WriteLine("\nГенерація та вивід завершено.");
