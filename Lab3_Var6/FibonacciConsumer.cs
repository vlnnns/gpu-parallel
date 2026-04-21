using static System.Console;

namespace Lab3_Var6;

public class FibonacciConsumer
{
    private readonly SharedBuffer _buffer;
    private readonly int _count;

    public FibonacciConsumer(SharedBuffer buffer, int count)
    {
        _buffer = buffer;
        _count = count;
    }

    public void Run()
    {
        long sum = 0;
        for (int i = 0; i < _count; i++)
        {
            _buffer.Filled.Wait();
            long value = _buffer.Current;
            sum += value;
            WriteLine($"#{i + 1}: число = {value}, сума = {sum}");
            _buffer.Empty.Release();
        }
    }
}
