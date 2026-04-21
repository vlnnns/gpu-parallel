namespace Lab3_Var6;

public class SharedBuffer
{
    public long Current { get; set; }

    public SemaphoreSlim Filled { get; } = new(0, 1);
    public SemaphoreSlim Empty { get; } = new(1, 1);
}
