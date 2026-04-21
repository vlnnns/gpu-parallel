using System.IO.MemoryMappedFiles;
using System.Text;
using static System.Console;

OutputEncoding = Encoding.UTF8;

const string MmfPath = "/tmp/lab4_color.dat";
const int MmfSize = 8;

using var mmf = MemoryMappedFile.CreateFromFile(
    MmfPath, FileMode.OpenOrCreate, null, MmfSize);
using var accessor = mmf.CreateViewAccessor(0, MmfSize);

var rnd = new Random();
int version = 0;

WriteLine("=== Writer ===");
WriteLine("ПРОБІЛ — згенерувати новий колір фону");
WriteLine("Q       — вихід");
WriteLine();

while (true)
{
    var key = ReadKey(intercept: true).Key;
    if (key == ConsoleKey.Q) break;
    if (key != ConsoleKey.Spacebar) continue;

    byte r = (byte)rnd.Next(256);
    byte g = (byte)rnd.Next(256);
    byte b = (byte)rnd.Next(256);
    version++;

    accessor.Write(0, r);
    accessor.Write(1, g);
    accessor.Write(2, b);
    accessor.Write(4, version);

    Write($"\x1b[48;2;{r};{g};{b}m\x1b[2J\x1b[H");
    WriteLine($"RGB = ({r}, {g}, {b})  hex = #{r:X2}{g:X2}{b:X2}  version = {version}");
    WriteLine("ПРОБІЛ — новий, Q — вихід");
}

Write("\x1b[0m\x1b[2J\x1b[H");
WriteLine("Writer завершено.");
