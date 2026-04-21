using System.IO.MemoryMappedFiles;
using System.Text;
using static System.Console;

OutputEncoding = Encoding.UTF8;

const string MmfPath = "/tmp/lab4_color.dat";
const int MmfSize = 8;

WriteLine("=== Reader ===");
while (!File.Exists(MmfPath))
{
    WriteLine($"Чекаю на створення {MmfPath} (запустіть Writer)...");
    Thread.Sleep(500);
}

using var mmf = MemoryMappedFile.CreateFromFile(
    MmfPath, FileMode.Open, null, MmfSize);
using var accessor = mmf.CreateViewAccessor(0, MmfSize);

WriteLine("Відстежую зміни кольору з Writer. Ctrl+C для виходу.");
WriteLine();

int lastVersion = 0;
while (true)
{
    int version = accessor.ReadInt32(4);

    if (version < lastVersion)
    {
        WriteLine("(Writer перезапущено — скидаємо лічильник)");
        lastVersion = 0;
    }

    if (version != lastVersion)
    {
        byte r = accessor.ReadByte(0);
        byte g = accessor.ReadByte(1);
        byte b = accessor.ReadByte(2);
        WriteLine($"#{version}: RGB = ({r}, {g}, {b})  hex = #{r:X2}{g:X2}{b:X2}");
        lastVersion = version;
    }
    Thread.Sleep(100);
}
