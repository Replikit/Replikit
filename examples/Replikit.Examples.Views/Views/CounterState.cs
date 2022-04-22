namespace Replikit.Examples.Views.Views;

public class CounterState
{
    public int Count { get; set; }

    public void Increment(int step = 1)
    {
        Count += step;
    }
}
