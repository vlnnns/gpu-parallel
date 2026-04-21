namespace Lab3_Var6;

public class FibonacciProducer
{
    private readonly SharedBuffer _buffer;
    private readonly int _count;

    public FibonacciProducer(SharedBuffer buffer, int count)
    {
        _buffer = buffer;
        _count = count;
    }

    public void Run()
    {
        long a = 0, b = 1;
        for (int i = 0; i < _count; i++)
        {
            long fib;
            if (i == 0) fib = 0;
            else if (i == 1) fib = 1;
            else
            {
                fib = a + b;
                a = b;
                b = fib;
            }

            _buffer.Empty.Wait();
            _buffer.Current = fib;
            _buffer.Filled.Release();

            if (i < _count - 1)
                Thread.Sleep(1000);
        }
    }
}
