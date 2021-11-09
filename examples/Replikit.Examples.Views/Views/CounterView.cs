using Replikit.Abstractions.Messages.Builder;
using Replikit.Extensions.Views;

namespace Replikit.Examples.Views.Views;

public class CounterView : View<CounterView.CounterState>
{
    public class CounterState
    {
        public int Count { get; set; }
    }

    public override ViewResult Render()
    {
        return CreateBuilder()
            .AddText($"Count: {State.Count}")
            .AddAction("+1", () => Increment(1))
            .AddAction("+10", () => Increment(10));
    }

    [Action]
    public void Increment(int step)
    {
        State.Count += step;
        Update();
    }
}
